
namespace SmartSpiderCore
{
    public class FieldResult
    {
        public string Title { get; set; }

        public string DataName { get; set; }

        public string DataValue { get; set; }

        public bool Require { get; set; }

        public int Sort { get; set; }
        
        public virtual bool Validation
        {
            get
            {
                if (Require && string.IsNullOrEmpty(DataValue))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
