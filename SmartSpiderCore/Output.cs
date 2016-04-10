using System;
using System.Collections.Generic;

namespace SmartSpiderCore
{
    public abstract class Output : IDisposable
    {
        public virtual void Init() { }

        public virtual void Write(ICollection<FieldResult> content) { }

        public virtual void Dispose()
        {
            
        }
    }
}
