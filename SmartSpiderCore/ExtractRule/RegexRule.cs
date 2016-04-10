using System.Text.RegularExpressions;

namespace SmartSpiderCore.ExtractRule
{
    public class RegexRule : Rule
    {
        public string RegexValue { get; set; }

        public override Content Exec(Content content)
        {
            content.ContentText = string.IsNullOrEmpty(content.ContentText) ? string.Empty : Regex.Match(content.ContentText, RegexValue).Value;

            return content;
        }
    }
}
