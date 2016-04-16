using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SmartSpiderCore.ExtractRule
{
    public class Md5Rule : Rule
    {
        public override Content Exec(Content content)
        {
            MD5 md5 = MD5.Create();

            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(content.ContentText));

            content.ContentText = "";

            for (int i = 0; i < result.Length; i++)
            {
                content.ContentText += result[i].ToString("x").PadLeft(2, '0');
            }

            return content;
        }
    }
}
