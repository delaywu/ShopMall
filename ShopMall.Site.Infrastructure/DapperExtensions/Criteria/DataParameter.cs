namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public class DataParameter
    {
        public DataParameter()
        {
        }
        public DataParameter(string name,object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
