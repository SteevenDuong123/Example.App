using Prism.Mvvm;

namespace Example.App.Model
{
    public class Product : BindableBase
    {
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }
        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { SetProperty(ref unitPrice, value); }
        }
        private int quality;
        public int Quality
        {
            get { return quality; }
            set { SetProperty(ref quality, value); }
        }
        private bool status;
        public bool Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
        private int code;
        public int Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }
    }
}
