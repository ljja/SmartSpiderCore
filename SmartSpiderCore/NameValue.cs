
namespace SmartSpiderCore
{
    public class NameValue
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public NameValue()
        {

        }

        public NameValue(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Name, Value);
        }
    }
}
