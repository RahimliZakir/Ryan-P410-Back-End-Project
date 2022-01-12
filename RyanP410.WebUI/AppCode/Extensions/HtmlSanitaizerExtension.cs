using System.Text.RegularExpressions;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class HExtension
    {
        public static string RemoveHtmlTags(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            Regex pattern = new Regex("<[^>]*>");

            return pattern.Replace(value, "");
        }
    }
}
