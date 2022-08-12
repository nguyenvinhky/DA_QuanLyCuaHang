using QuanLyCuaHang.Form;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLyCuaHang.ViewModel
{
    
    public class MainViewModel : BaseViewModel
    {
        public static bool chualuu = false;
        private BitmapImage _ImageNhanVien { get; set; }
        public BitmapImage ImageNhanVien { get=>_ImageNhanVien; set { _ImageNhanVien = value; OnPropertyChanged(); } }
        private string _TenNV { get; set; }
        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }
        private string _Quyen { get; set; }
        public string Quyen { get => _Quyen; set { _Quyen = value; OnPropertyChanged(); } }
        private string _IdNV { get; set; }
        public string IdNV { get => _IdNV; set { _IdNV = value; OnPropertyChanged(); } }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadViewSanPham { get; set; }
        public ICommand LoadViewTrangChu { get; set; }
        public ICommand LoadViewDonHang { get; set; }
        public ICommand LoadViewKhachHang { get; set; }
        public ICommand LoadViewNhapHang { get; set; }
        public ICommand LoadViewThongKe { get; set; }
        public ICommand LoadViewQuanLy { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand CheckHDNCommand { get; set; }


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
        public KhachHangViewModel KH { get; set; }
        public NhapHangViewModel NH { get; set; }
        public ThongKeViewModel TK { get; set; }
        public QuanLyViewModel QL { get; set; }

        public MainViewModel()
        {
            //Đăng nhập
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                CurrentView = TC;
            }
            );

            //Đăng xuất
            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                MainWindow dangnhap = new MainWindow();
                dangnhap.Show();
                p.Close();
            });

            //Load view
            SP = new SanPhamViewModel();
            TC = new TrangChuViewModel();
            DH = new DonHangViewModel();
            KH = new KhachHangViewModel();
            NH = new NhapHangViewModel();
            TK = new ThongKeViewModel();
            QL = new QuanLyViewModel();

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
                FormCreateHDB createHDB = new FormCreateHDB();
                var data = createHDB.DataContext as DonHangViewModel;
                data.TenNV = TenNV;
                data.IdNV = IdNV;
            }
            );

            LoadViewKhachHang = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = KH;
            }
            );

            LoadViewNhapHang = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = NH;
            }
            );

            LoadViewThongKe = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = TK;
            }
            );

            LoadViewQuanLy = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                CurrentView = QL;
            }
            );


            /*========================================= Hàm xử lý chức năng =========================================*/
        }
    }
}
