﻿namespace MimicWebApi.Utils;

public static class HttpContextExtensions
{
    public static long? GetOidcUserId(this HttpContext context) =>
        long.TryParse(context.User.FindFirst("oidc_id")?.Value, out long oidcId) ? oidcId : null;
}