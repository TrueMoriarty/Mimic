using DAL;
using MimicWebApi.VkAuth;
using MimicWebApi.VkAuth.Models;

namespace MimicWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var vkConfig = builder.Configuration.GetSection("VkConfig").Get<VkConfig>()!;
        builder.Services
            .AddAuthentication("vk-oauth-cookie")
            .AddCookie("vk-oauth-cookie")
            .AddVk("vk-oauth", "vk", o =>
            {
                o.ClientSecret = vkConfig.ClientId;
                o.ClientId = vkConfig.ClientId;
                o.CodeVerifier = vkConfig.CodeVerifier;
                o.CodeChallengeMethod = vkConfig.CodeChallengeMethod;

                o.AuthorizationEndpoint = "https://id.vk.com/authorize";
                o.TokenEndpoint = "https://id.vk.com/oauth2/auth";

                o.CallbackPath = "/api/auth/cb";
                o.UsePkce = true;
            });

        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                o =>
                {
                    o.WithOrigins(
                            "https://localhost:5173"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        // Add services to the container
        builder.Services.AddDAL();
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseCors(MyAllowSpecificOrigins);

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

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