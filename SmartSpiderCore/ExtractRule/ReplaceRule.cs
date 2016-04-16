
namespace SmartSpiderCore.ExtractRule
{
    public class ReplaceRule : Rule
    {
        public string Origin { get; set; }

        public string Target { get; set; }

        public override Content Exec(Content content)
        {
            content.ContentText = content.ContentText.Replace(Origin, Target);

            return content;
        }
    }
}
