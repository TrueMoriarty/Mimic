using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using MimicWebApi.VkAuth.Models;
using Services;

namespace MimicWebApi.VkAuth;

public class VkAuthenticationOptions : OAuthOptions
{
    public string? CodeVerifier { get; set; }
    public string? CodeChallengeMethod { get; set; }

    public VkAuthenticationOptions()
    {
        Events.OnRemoteFailure = (RemoteFailureContext context) =>
        {
            context.HandleResponse();
            return Task.CompletedTask;
        };

        Events.OnCreatingTicket = async (OAuthCreatingTicketContext context) =>
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
            var userService = context.HttpContext.RequestServices.GetService<IUserService>()!;
            var user = userService.GetByExternalId(userInfo.User.UserId);

            var clientLocation = context.HttpContext.RequestServices.GetService<IConfiguration>()!.GetValue<string>("ClientUrl")!;

            if (user is not null)
            {
                context.Identity.AddClaim(new("user_id", user.UserId.ToString()));
                context.Properties.RedirectUri = $"{clientLocation}/";
            }
            else
            {
                context.Identity.AddClaim(new("external_user_id", userInfo.User.UserId));
                context.Properties.RedirectUri = $"{clientLocation}/user/unbording";
            }
        };
    }
}