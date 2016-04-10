using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSpiderCore.In
{
    public class LocalDirectoryInput : Input
    {
        public string Path { get; set; }

        public string Pattern { get; set; }

        public string Encoding { get; set; }

        public override void Init()
        {
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return Directory.GetFiles(Path, Pattern, SearchOption.AllDirectories).Cast<string>().GetEnumerator();
        }

        public override Content GetContent(string uri)
        {
            return new Content
            {
                ContentText = File.ReadAllText(uri, System.Text.Encoding.GetEncoding(Encoding))
            };
        }

    }
}
