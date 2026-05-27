using Example.App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace Example.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties

        private string _title = "Quản lý Sản phẩm";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _productCode = "00001";
        public string ProductCode
        {
            get => _productCode;
            set => SetProperty(ref _productCode, value);
        }

        private string _productName = "Product 1";
        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }

        private bool _includeDeleted;
        public bool IncludeDeleted
        {
            get => _includeDeleted;
            set => SetProperty(ref _includeDeleted, value);
        }
        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (SetProperty(ref _isActive, value))
                {
                    MessageBox.Show(
                        $"Status: {(value ? "Active" : "Inactive")}");
                }
            }
        }

        public ObservableCollection<Product> Products { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand CommandSearch { get; private set; }
        public DelegateCommand DeleteRowCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }

        #endregion

        public MainWindowViewModel()
        {
            Products = new ObservableCollection<Product>();

            LoadSampleData();

            CommandSearch = new DelegateCommand(Search);
            DeleteRowCommand = new DelegateCommand(DeleteSelectedRow);
            UpdateCommand = new DelegateCommand(UpdateData);
        }

        private void LoadSampleData()
        {
            Products.Add(new Product
            {
                Code = "00001",
                Name = "Product 1",
                UnitPrice = 10000000,
                Quantity = 10,
                IsActive = true
            });

            Products.Add(new Product
            {
                Code = "00002",
                Name = "Product 2",
                UnitPrice = 20000000,
                Quantity = 20,
                IsActive = false
            });
        }

        private void Search()
        {

            MessageBox.Show("Tìm kiếm với:\n" +
                          $"Code: {ProductCode}\n" +
                          $"Name: {ProductName}\n" +
                          $"Include Deleted: {IncludeDeleted}", "Search");
        }

        private void DeleteSelectedRow()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa trong DataGrid!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm '{SelectedProduct.Name}' ?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Products.Remove(SelectedProduct);

                MessageBox.Show("Xóa dữ liệu thành công!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void UpdateData()
        {
            MessageBox.Show(IsActive.ToString());
            if (Products.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để cập nhật!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Dữ liệu đã được cập nhật thành công!",
                            "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}

