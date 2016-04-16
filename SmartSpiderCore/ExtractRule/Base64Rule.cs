using System;
using System.Text;

namespace SmartSpiderCore.ExtractRule
{
    public class Base64Rule : Rule
    {
        public override Content Exec(Content content)
        {
            content.ContentText = Convert.ToBase64String(Encoding.UTF8.GetBytes(content.ContentText));

            return content;
        }
    }
}
