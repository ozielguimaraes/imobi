using System.Text.RegularExpressions;

namespace Imobi.Extensions
{
    public static class StringExtension
    {
        public static string GetPersonName(this string text)
        {
            return Regex.Replace(text, Constants.Constants.Expressions.PersonName, string.Empty);
        }
    }
}