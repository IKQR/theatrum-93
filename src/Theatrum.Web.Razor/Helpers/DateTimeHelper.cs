using System;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Theatrum.Web.Razor.Helpers
{
    public static class DateTimeHelper
    {
        public static HtmlString NormalizeDate(this IHtmlHelper hh, DateTimeOffset date)
        {
            return new($"{date.Year}-{date.Month:D2}-{date.Day:D2}");
        }
        public static HtmlString NormalizeTime(this IHtmlHelper hh, DateTimeOffset date)
        {
            return new($"{date.Hour}:{date.Minute}");
        }
    }
}
