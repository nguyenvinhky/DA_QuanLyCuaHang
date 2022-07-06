using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadViewSanPham { get; set; }
        public ICommand LoadViewTrangChu { get; set; }
        public ICommand LoadViewDonHang { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public SanPhamViewModel SP { get; set; }
        public TrangChuViewModel TC { get; set; }
        public DonHangViewModel DH { get; set; }

        public MainViewModel()
        {
            //Load view sản phẩm
            SP = new SanPhamViewModel();
            TC = new TrangChuViewModel();
            DH = new DonHangViewModel();

            LoadViewTrangChu = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = TC;
            }
            );

            LoadViewSanPham = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = SP;
            }
            );

            LoadViewDonHang = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = DH;
            }
            );

        }
    }
}
