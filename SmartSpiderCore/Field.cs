using System.Collections.Generic;

namespace SmartSpiderCore
{
    public class Field
    {
        public string Title { get; set; }

        public string DataName { get; set; }

        public bool Require { get; set; }

        /// <summary>
        /// 主要规则
        /// </summary>
        public List<Rule> Rule { get; set; }

        /// <summary>
        /// 备用规则
        /// </summary>
        public List<Rule> Rule2 { get; set; }

        public int Index { get; set; }

        public int Sort { get; set; }

        public Field()
        {
            Title = string.Empty;
            DataName = string.Empty;
            Require = false;
            Rule = new List<Rule>();
            Rule2 = new List<Rule>();
        }

        public virtual Content Exec(Content content)
        {
            foreach (var rule in Rule)
            {
                content = rule.Exec(content);
            }
            return content;
        }

        public virtual Content Exec2(Content content)
        {
            foreach (var rule in Rule2)
            {
                content = rule.Exec(content);
            }
            return content;
        }
    }
}
