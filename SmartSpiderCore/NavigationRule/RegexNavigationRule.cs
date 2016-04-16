using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SmartSpiderCore
{
    public class RegexNavigationRule : NavigationRule
    {
		public string Prefix { get; set; }

        public string Suffix { get; set; }
		
        public string Begin { get; set; }

        public string End { get; set; }

        public string RegexValue { get; set; }

        public override ICollection<string> Exec(Content content)
        {
            var urlList = new List<string>();

            if (string.IsNullOrEmpty(Begin) == false && string.IsNullOrEmpty(End) == false)
            {
                content.ContentText = SubString(content.ContentText);
            }

            var matchCollection = Regex.Matches(content.ContentText, RegexValue);
            for (var i = 0; i < matchCollection.Count; i++)
            {
                urlList.Add(matchCollection[i].Value);
            }

            return Distinct(urlList);
        }

        private List<string> Distinct(IEnumerable<string> origin)
        {
            var tmp = new List<string>();

            foreach (var m in origin)
            {
                if (tmp.Contains(m) == false)
                {
                    tmp.Add(m);
                }
            }
			
			if (String.IsNullOrEmpty(Prefix) == false)
            {
                for (var i = 0; i < tmp.Count; i++)
                {
                    tmp[i] = Prefix + tmp[i];
                }
            }

            if (String.IsNullOrEmpty(Suffix) == false)
            {
                for (var i = 0; i < tmp.Count; i++)
                {
                    tmp[i] = tmp[i] + Suffix;
                }
            }

            return tmp;
        }

        private string SubString(string content)
        {
            var startIndex = content.IndexOf(Begin, StringComparison.Ordinal) + Begin.Length;

            if (startIndex < Begin.Length)
            {
                return string.Empty;
            }

            var endIndex = content.IndexOf(End, startIndex, StringComparison.Ordinal);

            if (endIndex <= startIndex)
            {
                return string.Empty;
            }

            return content.Substring(startIndex, endIndex - startIndex);
        }
    }
}
