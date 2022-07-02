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

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Main LoginWindow = new Main();
                LoginWindow.ShowDialog();
            }
              );

            //Load view sản phẩm
            LoadViewSanPham = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                Main m = new Main();
                m.GridViewMain.Children.Clear();
                m.GridViewMain.Children.Add(new View.SanPham());
            }
              );
        }
    }
}
