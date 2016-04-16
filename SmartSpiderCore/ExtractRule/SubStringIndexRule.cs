using System;
using System.Text.RegularExpressions;

namespace SmartSpiderCore.ExtractRule
{
    public class SubStringIndexRule : Rule
    {
        public string RegexValue { get; set; }

        public string End { get; set; }

        public int Index { get; set; }

        public override Content Exec(Content content)
        {
            var matchCollection = Regex.Matches(content.ContentText, RegexValue);

            if (matchCollection.Count < Index)
            {
                content.ContentText = string.Empty;
                return content;
            }

            var startIndex = matchCollection[Index].Index + matchCollection[Index].Length;

            if (startIndex > content.ContentText.Length)
            {
                content.ContentText = string.Empty;
                return content;
            }

            var endIndex = content.ContentText.IndexOf(End, startIndex, StringComparison.Ordinal);

            if (endIndex <= startIndex)
            {
                content.ContentText = string.Empty;
                return content;
            }

            content.ContentText = content.ContentText.Substring(startIndex, endIndex - startIndex);

            return content;
        }
    }
}
