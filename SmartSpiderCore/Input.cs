using System.Collections.Generic;

namespace SmartSpiderCore
{
    public abstract class Input
    {
        public abstract void Init();

        public abstract IEnumerator<string> GetEnumerator();

        public abstract Content GetContent(string uri);

    }
}
