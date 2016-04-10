using System;
using System.Linq;

namespace SmartSpiderCore.ExtractRule
{
    public class SessionRule : Rule
    {
        public string Key { get; set; }

        public override Content Exec(Content content)
        {
            if (content.Session != null && content.Session.Count > 0)
            {
                var first = content.Session.FirstOrDefault(p => string.Compare(p.Name, Key, StringComparison.CurrentCultureIgnoreCase) == 0);
                if (first != null)
                {
                    content.ContentText = first.Value;

                    return content;
                }
            }

            content.ContentText = string.Empty;

            return content;
        }
    }
}
