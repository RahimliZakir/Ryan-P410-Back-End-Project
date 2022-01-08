using System.Text.RegularExpressions;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsEmail(this string content)
        {
            Regex pattern = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (!pattern.IsMatch(content))
                return false;

            return true;
        }
    }
}
