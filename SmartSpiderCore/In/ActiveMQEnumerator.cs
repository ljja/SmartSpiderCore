using System.Collections;
using System.Collections.Generic;

namespace SmartSpiderCore.In
{
    public class ActiveMQEnumerator : IEnumerator<string>
    {
        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
            
        }

        public string Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
