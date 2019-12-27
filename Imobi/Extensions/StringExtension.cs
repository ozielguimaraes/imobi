using System.Text;
using System.Text.RegularExpressions;

namespace Imobi.Extensions
{
    public static class StringExtension
    {
        #region Public Methods

        public static string GetPersonName(this string text)
        {
            return Regex.Replace(text, Constants.Constants.Expressions.PersonName, string.Empty);
        }

        public static string NumbersOnly(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            string pattern = @"\d";

            var sb = new StringBuilder();

            foreach (Match m in Regex.Matches(input, pattern)) sb.Append(m);

            return sb.ToString();
        }

        #endregion Public Methods
    }
}