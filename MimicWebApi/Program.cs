using Amazon.Runtime;
using Amazon.S3;
using DAL;
using MimicWebApi.ConfigModels;
using MimicWebApi.VkAuth;
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

        var s3Config = builder.Configuration.GetSection("S3").Get<S3Config>()!;
        var vkConfig = builder.Configuration.GetSection("VkConfig").Get<VkConfig>()!;
        string clientLocation = builder.Configuration.GetValue<string>("ClientUrl")!;
        
        builder.Services.AddSerilog();
        builder.Services.AddSingleton<IAmazonS3>(_ =>
        {
            BasicAWSCredentials awsCreds = new(s3Config.AccessKey, s3Config.SecretKey);

            AmazonS3Config S3Config = new()
            {
                ServiceURL = s3Config.Url,
                UseHttp = true,
                ForcePathStyle = true,
                AuthenticationRegion = s3Config.Region,
            };

            return new AmazonS3Client(awsCreds, S3Config);
        });

        builder.Services.AddDAL();
        builder.Services.AddServices();
        builder.Services
            .AddAuthentication("auth-cookie")
            .AddCookie("auth-cookie", o =>
            {
                o.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
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