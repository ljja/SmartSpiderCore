using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSpiderCore.In
{
    public class FileLineEnumeratorRule : EnumeratorRule
    {
        private int _index = -1;

        public List<string> Url { set; get; }

        public string FileName { get; set; }

        public string Encoding { get; set; }

        public FileLineEnumeratorRule()
        {
            Url = new List<string>();

            if (string.IsNullOrEmpty(Encoding)) this.Encoding = "utf-8";

            if (string.IsNullOrEmpty(FileName) == false)
            {
                Url = File.ReadAllLines(FileName, System.Text.Encoding.GetEncoding(Encoding)).ToList();
            }
        }

        public override Content Exec(Content content)
        {
            return new Content { ContentText = this.Current };
        }

        public override string Current
        {
            get
            {
                return Url[_index];
            }
        }

        public override bool MoveNext()
        {
            if (Url.Any() == false)
            {
                if (string.IsNullOrEmpty(Encoding)) this.Encoding = "utf-8";

                if (string.IsNullOrEmpty(FileName) == false)
                {
                    Url = File.ReadAllLines(FileName, System.Text.Encoding.GetEncoding(Encoding)).ToList();
                }
            }

            _index += 1;

            if (_index < Url.Count)
            {
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            _index = -1;
        }
    }
}
