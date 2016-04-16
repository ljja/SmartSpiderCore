
namespace SmartSpiderCore.ExtractRule
{
    public class TrimRule : Rule
    {
        public override Content Exec(Content content)
        {
            content.ContentText = content.ContentText.Replace("  ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();

            return content;
        }
    }
}
