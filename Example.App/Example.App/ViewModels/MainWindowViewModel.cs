using Example.App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Example.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties
        private string _title = "Manager Production";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _productCode;
        public string ProductCode
        {
            get => _productCode;
            set { SetProperty(ref _productCode, value); ApplyFilter(); }
        }

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set { SetProperty(ref _productName, value); ApplyFilter(); }
        }

        private bool _includeDeleted = false;
        public bool IncludeDeleted
        {
            get => _includeDeleted;
            set { SetProperty(ref _includeDeleted, value); ApplyFilter(); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        public ObservableCollection<Product> Products { get; private set; } = [];
        public ObservableCollection<Product> FilteredProducts { get; private set; } = [];
        //public ObservableCollection<Product> DeletedProducts { get; private set; } = [];    

        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Products.txt");

        #endregion

        #region Commands
        public DelegateCommand CommandSearch { get; private set; }
        public DelegateCommand DeleteRowCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        #endregion

        public MainWindowViewModel()
        {
            CommandSearch = new DelegateCommand(Search);
            DeleteRowCommand = new DelegateCommand(DeleteSelectedRow);
            UpdateCommand = new DelegateCommand(UpdateData);
            LoadDataFromTextFile();
        }

        #region Search & Filter
        private void Search()
        {
            if (string.IsNullOrWhiteSpace(ProductCode) && string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Vui lòng nhập ít nhất một tiêu chí tìm kiếm (Product Code hoặc Product Name).",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ApplyFilter();

            if (FilteredProducts.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu phù hợp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ApplyFilter()
        {
            FilteredProducts.Clear();

            var query = Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(ProductCode))
                query = query.Where(p => p.Code?.IndexOf(ProductCode.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

            if (!string.IsNullOrWhiteSpace(ProductName))
                query = query.Where(p => p.Name?.IndexOf(ProductName.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

            if (!IncludeDeleted)
                query = query.Where(p => p.IsActive);

            foreach (var item in query.ToList())
                FilteredProducts.Add(item);
        }
        #endregion

        #region File Operations
        private void LoadDataFromTextFile()
        {
            Products.Clear();
            try
            {
                if (!File.Exists(_filePath))
                    CreateSampleDataFile();

                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines.Skip(1))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split('|');
                    if (parts.Length >= 5)
                    {
                        Products.Add(new Product
                        {
                            Code = parts[0].Trim(),
                            Name = parts[1].Trim(),
                            UnitPrice = decimal.TryParse(parts[2].Trim(), out var price) ? price : 0,
                            Quantity = int.TryParse(parts[3].Trim(), out var qty) ? qty : 0,
                            IsActive = bool.TryParse(parts[4].Trim(), out var active) && active
                        });
                    }
                }
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file:\n{ex.Message} + {ex.StackTrace}");
            }
        }

        private void CreateSampleDataFile()
        {
            var sample = @"Code|Name|UnitPrice|Quantity|IsActive
                            00002|Product 2|20000000|20|False
                            00001|Product 1|10000000|10|True
                            00003|Product 3|15000000|15|True";
            File.WriteAllText(_filePath, sample);
        }

        private void SaveToTextFile()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                {
                    writer.WriteLine("Code|Name|UnitPrice|Quantity|IsActive");
                    foreach (var p in Products)
                    {
                        writer.WriteLine($"{p.Code}|{p.Name}|{p.UnitPrice}|{p.Quantity}|{p.IsActive}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu file:\n{ex.Message}");
            }
        }
        #endregion

        #region Command Methods
        private void DeleteSelectedRow()
        {
            try
            {
                if (SelectedProduct == null)
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (MessageBox.Show($"Xóa '{SelectedProduct.Name}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Products.Remove(SelectedProduct);
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Have a error for data with is {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    
            }
           
        }

        private void UpdateData()
        {

            SyncFilteredToProducts();
            SaveToTextFile();
            MessageBox.Show("Dữ liệu đã được lưu vào file thành công!", "Thành công", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }


        private void SyncFilteredToProducts()
        {

            var currentCodes = FilteredProducts.Select(p => p.Code).ToList();

            var itemsToRemove = Products.Where(p => !currentCodes.Contains(p.Code)).ToList();
            foreach (var item in itemsToRemove)
            {
                Products.Remove(item);
            }


            foreach (var item in FilteredProducts)
            {
                if (!Products.Any(p => p.Code == item.Code))
                {
                    Products.Add(item);
                }
            }
        }
        #endregion
    }
}