using Prism.Mvvm;

namespace Example.App.Model
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
