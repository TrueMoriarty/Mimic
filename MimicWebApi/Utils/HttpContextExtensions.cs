namespace MimicWebApi.Utils;

public static class HttpContextExtensions
{
    public static string? GetExternalUserId(this HttpContext context) =>
        context.User.FindFirst("external_user_id")?.Value;

    public static int? GetUserId(this HttpContext context) =>
        int.TryParse(context.User.FindFirst("user_id")?.Value, out int id) ? id : null;
}