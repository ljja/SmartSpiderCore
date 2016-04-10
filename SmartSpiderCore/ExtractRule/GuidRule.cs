using System;

namespace SmartSpiderCore.ExtractRule
{
    public class GuidRule : Rule
    {
        public override Content Exec(Content content)
        {
            content.ContentText = Guid.NewGuid().ToString();

            return content;
        }
    }
}
