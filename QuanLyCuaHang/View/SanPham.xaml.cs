using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyCuaHang.View
{
    /// <summary>
    /// Interaction logic for SanPham.xaml
    /// </summary>
    public partial class SanPham : UserControl
    {
        public SanPham()
        {
            InitializeComponent();
            var product = GetProduct();
            if (product.Count > 0)
            {
                ListViewProducts.ItemsSource = product;
            }
        }

        private List<Model.Product> GetProduct()
        {
            return new List<Model.Product>()
            {
                new Model.Product("Áo thun", 453, "/Anh/4.jpg"),
                new Model.Product("Áo thun", 250.46, "/Anh/2.jpg"),
                new Model.Product("Áo thun 3", 567, "/Anh/3.jpg"),
                new Model.Product("Áo thun 4", 123, "/Anh/4.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/5.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/8.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/7.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/8.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/4.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/2.jpg"),
                new Model.Product("Áo thun 5", 12.23, "/Anh/7.jpg"),
            };
        }
    }
}
