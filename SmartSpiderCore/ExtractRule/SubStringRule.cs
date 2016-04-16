using System;

namespace SmartSpiderCore.ExtractRule
{
    public class SubStringRule : Rule
    {
        public string Begin { get; set; }

        public string End { get; set; }
        
        public override Content Exec(Content content)
        {
            var startIndex = content.ContentText.IndexOf(Begin, StringComparison.Ordinal) + Begin.Length;

            if (startIndex < Begin.Length)
            {
                return new Content();
            }

            var endIndex = content.ContentText.IndexOf(End, startIndex, StringComparison.Ordinal);

            if (endIndex <= startIndex)
            {
                return new Content();
            }

            content.ContentText = content.ContentText.Substring(startIndex, endIndex - startIndex);
            
            return content;
        }
    }
}
