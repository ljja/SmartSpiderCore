using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SmartSpiderCore.ExtractRule
{
    public class InnerTextRule : Rule
    {
        public override Content Exec(Content content)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(content.ContentText);

            content.ContentText = doc.DocumentNode.InnerText;

            return content;
        }
    }
}
