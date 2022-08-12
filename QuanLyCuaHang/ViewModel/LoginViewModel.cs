using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLyCuaHang.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        private string directory = "";
        private string _Username { get; set; }
        public string Username { get=>_Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password { get; set; }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public bool isLogin = false;
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {

            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            Password = "";
            Username = "";

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => 
            {
                this.Password = p.Password.ToString();
            });
        }

        void Login(Window p)
        {
            if (p == null)
                return;
            string passEndcode = MD5Hash(Base64Encode(this.Password));
            var LG = DataProvider.Ins.DB.TaiKhoans.Where(x=>x.Username==Username && x.Password==passEndcode).SingleOrDefault();
            if (LG != null)
            {
                Main main = new Main();
                var LoadNV = main.DataContext as MainViewModel;
                LoadNV.ImageNhanVien = new BitmapImage(new Uri(directory + LG.NhanVien.Anh));
                LoadNV.TenNV = LG.NhanVien.TenNV;
                LoadNV.Quyen = LG.LoaiTK.TenLoaiTK;
                LoadNV.IdNV = LG.NhanVien.Id;
                main.Show();
                MessageBox.Show("Đăng nhập thành công !", "Thông báo");
                p.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu !", "Thông báo");
                return;
            }
            
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
