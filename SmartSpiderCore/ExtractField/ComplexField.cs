using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartSpiderCore.ExtractField
{
    public class ComplexField : Field
    {
        public string SplitText { get; set; }
        
        public List<Field> SubField { get; set; }

        public override Content Exec(Content content)
        {
            content = base.Exec(content);

            content.ContentText = content.ContentText.Replace(SplitText, ((char)1).ToString(CultureInfo.InvariantCulture));

            var splitChar = new[] { (char)1 };
            var splitContent = content.ContentText.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("[");
            foreach (var splitItem in splitContent)
            {
                stringBuilder.Append("{");
                foreach (var subField in SubField)
                {
                    var splitItemContent = new Content { ContentText = splitItem };
                    
                    foreach (var rule in subField.Rule)
                    {
                        splitItemContent = rule.Exec(splitItemContent);
                    }


                    stringBuilder.AppendFormat("\"{0}\":\"{1}\",", subField.DataName, splitItemContent);
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("},");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("]");

            content.ContentText = stringBuilder.ToString();
            
            return content;
        }
    }
}
