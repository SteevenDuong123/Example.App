using Prism.Mvvm;

namespace Example.App.Model
{
    public class Product : BindableBase
    {
        //Chưa fix for database only demo
        private string _code;
        private string _name;
        private decimal _unitPrice;
        private int _quantity;
        private bool _isActive = true;

        public string Code { get => _code; set => SetProperty(ref _code, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public decimal UnitPrice { get => _unitPrice; set => SetProperty(ref _unitPrice, value); }
        public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
    }
}
