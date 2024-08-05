namespace MimicWebApi.Utils;

public static class HttpContextExtensions
{
    public static string? GetExternalUserId(this HttpContext context) =>
        context.User.FindFirst("external_user_id")?.Value;
    public static long? GetUserId(this HttpContext context) =>
        long.TryParse(context.User.FindFirst("user_id")?.Value, out long oidcId) ? oidcId : null;
}