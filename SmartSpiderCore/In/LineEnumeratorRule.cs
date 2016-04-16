using System.Collections.Generic;

namespace SmartSpiderCore.In
{
    public class LineEnumeratorRule : EnumeratorRule
    {
        private int _index = -1;
        public List<string> Url { set; get; }

        public LineEnumeratorRule()
        {
            Url = new List<string>();
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
