using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSpiderCore.ExtractRule
{
    public class FixedValueRule : Rule
    {
        public string Text { get; set; }

        public override Content Exec(Content content)
        {
            content.ContentText = Text;

            return content;
        }
    }
}
