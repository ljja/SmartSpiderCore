using System;
using System.Collections.Generic;

namespace SmartSpiderCore
{
    public class Content : ICloneable 
    {
        public string ContentText { get; set; }

        public List<NameValue> Session { get; set; }

        public Content()
        {
            Session = new List<NameValue>();
            ContentText = string.Empty;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
