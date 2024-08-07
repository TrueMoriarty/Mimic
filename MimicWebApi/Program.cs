using DAL;
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