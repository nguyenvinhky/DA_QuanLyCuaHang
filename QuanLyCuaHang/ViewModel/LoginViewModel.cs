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
using System.Net;
using System.Net.Mail;
using QuanLyCuaHang.Form;

namespace QuanLyCuaHang.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        private string _EmailAccount { get; set; }
        public string EmailAccount { get => _EmailAccount; set { _EmailAccount = value; OnPropertyChanged(); } }
        private string directory = "";
        private string _Username { get; set; }
        public string Username { get=>_Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password { get; set; }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public bool isLogin = false;
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ForgotPassword { get; set; }
        public ICommand SendEmail { get; set; }
        public LoginViewModel()
        {
            Random rand = new Random();
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            Password = "";
            Username = "";

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => 
            {
                this.Password = p.Password.ToString();
            });

            ForgotPassword = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                FormForgotEmail formForgotEmail = new FormForgotEmail();
                formForgotEmail.ShowDialog();
            });

            SendEmail = new RelayCommand<object>((p) => 
            {
                return true; 
            }, (p) =>
            {
                var KtrDangKy = DataProvider.Ins.DB.TaiKhoans.Where(x=>x.NhanVien.Email == EmailAccount).SingleOrDefault();
                if (KtrDangKy != null)
                {
                    int newPass = rand.Next(100000, 999999);
                    KtrDangKy.Password = MD5Hash(Base64Encode(newPass.ToString()));
                    DataProvider.Ins.DB.SaveChanges();
                    try
                    {
                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.EnableSsl = true;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("vinhky0167@gmail.com", "zthangqlxnqwuobv");

                            MailMessage mail = new MailMessage();
                            mail.To.Add(EmailAccount);
                            mail.From = new MailAddress("vinhky0167@gmail.com");
                            mail.Subject = "LẤY LẠI MẬT KHẨU";
                            mail.Body = $"Mật khẩu mới của bạn là: {newPass}. Trân trọng !";

                            smtp.Send(mail);
                            MessageBox.Show($"Đã gửi mật khẩu vào Email {EmailAccount} đã đăng ký", "Thông báo");
                        }
                    }
                    catch { }
                }
                else
                {
                    MessageBox.Show("Email của bạn chưa đúng vui lòng nhập lại !!!", "Thông báo");
                    EmailAccount = "";
                }
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
