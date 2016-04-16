using System.Collections.Generic;

namespace SmartSpiderCore
{
    public abstract class EnumeratorRule : Rule, IEnumerator<string>
    {
        public abstract string Current { get; }

        public virtual void Dispose() { }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
