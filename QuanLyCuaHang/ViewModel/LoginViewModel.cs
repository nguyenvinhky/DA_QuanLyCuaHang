using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        public bool isLogin = false;
        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            isLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
        }

        void Login(Window p)
        {
            Main main = new Main();
            main.Show();
            isLogin = true;
            p.Close();
        }
    }
}
