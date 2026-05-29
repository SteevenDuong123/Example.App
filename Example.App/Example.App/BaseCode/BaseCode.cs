using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Example.App.BaseCode
{
    public static class BaseCode
    {

        private static void Search(string productCode, string productName)
        {
            if (string.IsNullOrWhiteSpace(productCode) && string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Vui lòng nhập ít nhất một tiêu chí tìm kiếm (Product Code hoặc Product Name).",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //ApplyFilter();

            //if (FilteredProducts.Count == 0)
            //{
            //    MessageBox.Show("Không tìm thấy dữ liệu phù hợp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}
