using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSpiderCore.Out
{
    public class SOHOutput : Output
    {
        public string Path { get; set; }

        public override void Write(ICollection<FieldResult> content)
        {
            var stringBuilder = new StringBuilder();

            if (File.Exists(Path) == false)
            {
                File.WriteAllText(Path, "");
                foreach (var m in content)
                {
                    stringBuilder.AppendFormat("{0}{1}", m.DataName, (char)1);
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("\r\n");
            }

            foreach (var m in content)
            {
                stringBuilder.AppendFormat("{0}{1}", m.DataValue, (char)1);
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("\r\n");

            File.AppendAllText(Path, stringBuilder.ToString(), Encoding.UTF8);
        }
    }
}
