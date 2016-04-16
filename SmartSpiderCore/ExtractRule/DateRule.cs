using System;
using System.Globalization;

namespace SmartSpiderCore.ExtractRule
{
    public class DateRule : Rule
    {
        public string Format { get; set; }

        public override Content Exec(Content content)
        {
            content.ContentText = string.IsNullOrEmpty(Format) ? DateTime.Now.ToString(CultureInfo.InvariantCulture) : DateTime.Now.ToString(Format);

            return content;
        }
    }
}
