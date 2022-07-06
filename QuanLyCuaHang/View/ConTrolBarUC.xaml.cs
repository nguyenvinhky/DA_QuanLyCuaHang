using QuanLyCuaHang.ViewModel;
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
    /// Interaction logic for ConTrolBarUC.xaml
    /// </summary>
    public partial class ConTrolBarUC : UserControl
    {
        public ControlBarViewModel viewmodel { get; set; }
        public ConTrolBarUC()
        {
            InitializeComponent();
            this.DataContext = viewmodel = new ControlBarViewModel();
        }
    }
}
