using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartSpiderCore.ExtractRule
{
    public class RegexToList : Rule
    {
        public string RegexValue { get; set; }

        public string FileName { get; set; }

        public override Content Exec(Content content)
        {
            if (string.IsNullOrEmpty(content.ContentText))
            {
                content.ContentText = String.Empty;

                return content;
            }

            var resultList = new List<string>();
            var matcheCollection = Regex.Matches(content.ContentText, RegexValue);

            for (var i = 0; i < matcheCollection.Count; i++)
            {
                resultList.Add(matcheCollection[i].Value);
            }

            resultList = resultList.Distinct().ToList();

            if (File.Exists(FileName) == false)
            {
                File.AppendAllText(FileName, String.Empty, Encoding.UTF8);
            }

            using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write))
            {
                using (var write = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var m in resultList)
                    {
                        write.WriteLine(m);
                    }

                    write.Close();
                    write.Dispose();
                }

                fs.Close();
                fs.Dispose();
            }

            content.ContentText = String.Empty;

            return content;
        }
    }
}
