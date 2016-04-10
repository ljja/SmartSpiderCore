using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSpiderCore.In
{
    public class FileInput : Input
    {
        public string Path { get; set; }

        public string Encoding { get; set; }

        public override void Init()
        {
            
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return File.ReadAllLines(Path, System.Text.Encoding.GetEncoding(Encoding)).Cast<string>().GetEnumerator();
        }

        public override Content GetContent(string uri)
        {
            return new Content { ContentText = uri };
        }
    }
}
