using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using MimicWebApi.VkAuth;
using MimicWebApi.VkAuth.Models;
using Serilog;
using Services;

namespace MimicWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSerilog();
        builder.Services.AddDAL();
        builder.Services.AddServices();

        var vkConfig = builder.Configuration.GetSection("VkConfig").Get<VkConfig>()!;
        string clientLocation = builder.Configuration.GetValue<string>("ClientUrl")!;

        builder.Services
            .AddAuthentication("auth-cookie")
            .AddCookie("auth-cookie")
            .AddVk("vk-oauth", "vk", o =>
            {
                o.SignInScheme = "auth-cookie";
                o.ClientSecret = vkConfig.ClientId;
                o.ClientId = vkConfig.ClientId;
                o.CodeVerifier = vkConfig.CodeVerifier;
                o.CodeChallengeMethod = vkConfig.CodeChallengeMethod;

                o.AuthorizationEndpoint = "https://id.vk.com/authorize";
                o.TokenEndpoint = "https://id.vk.com/oauth2/auth";
                o.UserInformationEndpoint = "https://id.vk.com/oauth2/user_info";

                o.Scope.Add("vkid.personal_info");

                o.CallbackPath = "/api/auth/cb";
                o.UsePkce = true;

                o.Events.OnRemoteFailure = (RemoteFailureContext context) =>
                {
                    context.HandleResponse();
                    return Task.CompletedTask;
                };

                o.Events.OnCreatingTicket = async (OAuthCreatingTicketContext context) =>
                {
                    using var request = new HttpRequestMessage(HttpMethod.Post, context.Options.UserInformationEndpoint);

                    var dict = new Dictionary<string, string>
                    {
                    { "client_id", context.Options.ClientId },
                    { "access_token", context.AccessToken }
                    };

                    request.Content = new FormUrlEncodedContent(dict);

                    request.Version = context.Backchannel.DefaultRequestVersion;

                    using var response = await context.Backchannel.SendAsync(request);

                    var userInfo = await response.Content.ReadFromJsonAsync<VkUserInfo>();
                    var userService = context.HttpContext.RequestServices.GetService<IUserService>();

                    var id = long.Parse(userInfo.User.UserId);
                    var user = userService.GetByOidcId(id);

                    if (user is not null)
                    {
                        context.Identity.AddClaim(new("user_id", user.UserId.ToString()));
                        context.Properties.RedirectUri = $"{clientLocation}/";
                    }
                    else
                    {
                        context.Identity.AddClaim(new("oidc_id", userInfo.User.UserId));
                        context.Properties.RedirectUri = $"{clientLocation}/user/unbording";
                    }
                };
            });

        builder.Services.AddAuthorization();

        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                o =>
                {
                    o.WithOrigins(clientLocation)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        // Add services to the container
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        app.Run();
    }
}