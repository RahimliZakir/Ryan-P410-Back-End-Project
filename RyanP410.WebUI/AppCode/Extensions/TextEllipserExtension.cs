namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string EllipseText(this string text, int max = 100)
        {
            if (text.Length < max)
                return text;

            if (text.Length == max)
                return text;

            return $"{text.Substring(0, max)}...";
        }
    }
}
