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

namespace QuanLyCuaHang.ViewModel
{
    
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadViewSanPham { get; set; }
        public ICommand LoadViewTrangChu { get; set; }
        public ICommand LoadViewDonHang { get; set; }
        public ICommand LoadViewKhachHang { get; set; }
        public ICommand LoadViewNhapHang { get; set; }
        public ICommand LoadViewThongKe { get; set; }
        public ICommand LoadViewQuanLy { get; set; }
        public ICommand LogOutCommand { get; set; }


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
                if (p == null)
                    return;

                p.Hide();

                MainWindow login = new MainWindow();
                login.ShowDialog();

                var LoginVM = login.DataContext as LoginViewModel;

                if (LoginVM.isLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
            );

            //Đăng xuất
            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                functionLogOut(p);
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
            void functionLogOut(Window p)
            {
                p.Hide();
                MainWindow FormLogIn = new MainWindow();
                FormLogIn.ShowDialog();
            }
        }
    }
}
