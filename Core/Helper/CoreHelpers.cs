using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class CoreHelpers
    {
        private readonly static string _appVersion = Guid.NewGuid().ToString();

        public static string AppVersion() => _appVersion;

        public static string ReviewText(this string text, int maxsize)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            if (text.Length < maxsize)
                return text;
            return text.Substring(0, maxsize) + "...";
        }

        public static string HtmlToPlainText(string html)
        {
            if (string.IsNullOrEmpty(html))
                return "";
            html = html.Trim();
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            text = System.Net.WebUtility.HtmlDecode(text);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
