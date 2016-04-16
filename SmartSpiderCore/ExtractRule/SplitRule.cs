using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSpiderCore.ExtractRule
{
    public class SplitRule : Rule
    {
        public char SplitChar { get; set; }

        public int ValueIndex { get; set; }

        public override Content Exec(Content content)
        {
            var splitResult = content.ContentText.Split(SplitChar);

            if (splitResult.Length > ValueIndex)
            {
                content.ContentText = splitResult[ValueIndex];

                return content;
            }

            content.ContentText = String.Empty;

            return content;
        }
    }
}
