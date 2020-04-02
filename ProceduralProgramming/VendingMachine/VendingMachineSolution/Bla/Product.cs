namespace Bla
{
    public class Product
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public decimal Price {get; set;}
        public int Stock {get; set;}
        public Product(string key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}
