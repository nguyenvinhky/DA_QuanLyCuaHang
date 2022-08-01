using QuanLyCuaHang.Form;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    public class NhapHangViewModel : BaseViewModel
    {
        private ObservableCollection<Model.HDN> _ListHDN { get; set; }
        public ObservableCollection<Model.HDN> ListHDN { get => _ListHDN; set { _ListHDN = value; OnPropertyChanged(); } }
        public ICommand CreateHDN { get; set; }
        public NhapHangViewModel()
        {
            //Danh sách hóa đơn nhập
            ListHDN = new ObservableCollection<Model.HDN>(DataProvider.Ins.DB.HDNs);

            CreateHDN = new RelayCommand<Window>((p)=> 
            { 
                return true;
            }, (p)=> 
            {
                FormThemHoaDon addHD = new FormThemHoaDon();
                addHD.ShowDialog();
            });
        }
    }
}
