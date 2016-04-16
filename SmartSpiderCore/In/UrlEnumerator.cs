using System.Collections.Generic;
using System.Xml.Serialization;

namespace SmartSpiderCore.In
{
    [XmlInclude(typeof(ForEnumeratorRule)), XmlInclude(typeof(LineEnumeratorRule))]
    public class UrlEnumerator : IEnumerator<string>
    {
        public string UrlFormat { get; set; }

        public List<EnumeratorRule> EnumeratorRuleList { get; set; }

        public string Current
        {
            get
            {
                var content = new Content { ContentText = UrlFormat };

				foreach (var rule in EnumeratorRuleList) {
					content = rule.Exec (content);
				}

                return content.ContentText;
            }
        }

        public void Dispose()
        {

        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            foreach (var m in EnumeratorRuleList)
            {
                if (m.MoveNext() == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            foreach (var m in EnumeratorRuleList)
            {
                m.Reset();
            }
        }
    }
}
