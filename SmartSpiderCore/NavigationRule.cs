using System.Collections.Generic;

namespace SmartSpiderCore
{
    public abstract class NavigationRule
    {
        public abstract ICollection<string> Exec(Content content);
    }
}
