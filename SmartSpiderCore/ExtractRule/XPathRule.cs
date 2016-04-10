using HtmlAgilityPack;

namespace SmartSpiderCore.ExtractRule
{
    public class XPathRule : Rule
    {
        public string XPathValue { get; set; }

        public override Content Exec(Content content)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(content.ContentText);

            content.ContentText = doc.DocumentNode.SelectSingleNode(XPathValue).OuterHtml;

            return content;
        }
    }
}
