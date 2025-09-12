using Blazor.Extensions.Components.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Diagnostics;
using System.Globalization;

namespace Blazor.Extensions.Components.Helpers;

public static class LocalizationHelper
{
    public static void AddLocalizationCookie(HttpContext? httpContext)
    {
        httpContext?.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
            new RequestCulture(
                CultureInfo.CurrentCulture,
                CultureInfo.CurrentUICulture)));
    }
}
