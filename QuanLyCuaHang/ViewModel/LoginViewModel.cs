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
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace QuanLyCuaHang.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        string IntroIDNV = "NVSJDI_";
        string fileiamge = null;
        string dialog;
        private string nameImages;
        public string addimages;
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _TenNV;
        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _NgaySinh = DateTime.Now.Date;
        public Nullable<System.DateTime> NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private BitmapImage _Images;
        public BitmapImage Images { get => _Images; set { _Images = value; OnPropertyChanged(); } }
        private string _EmailAccount { get; set; }
        public string EmailAccount { get => _EmailAccount; set { _EmailAccount = value; OnPropertyChanged(); } }
        private string directory = "";
        private string _Username { get; set; }
        public string Username { get=>_Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password { get; set; }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _PasswordDangKy { get; set; }
        public string PasswordDangKy { get => _PasswordDangKy; set { _PasswordDangKy = value; OnPropertyChanged(); } }
        public bool isLogin = false;
        private ObservableCollection<NhanVien> _NhanVienList;
        public ObservableCollection<NhanVien> NhanVienList { get => _NhanVienList; set { _NhanVienList = value; OnPropertyChanged(); } }
        private ObservableCollection<ObjectGioiTinh> _ListGioiTinh;
        public ObservableCollection<ObjectGioiTinh> ListGioiTinh { get => _ListGioiTinh; set { _ListGioiTinh = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ForgotPassword { get; set; }
        public ICommand SendEmail { get; set; }
        public ICommand DangKy { get; set; }
        public ICommand DangKyNhanVien { get; set; }
        public ICommand ChooseImages { get; set; }
        public LoginViewModel()
        {
            // GIỚI TÍNH
            ListGioiTinh = new ObservableCollection<ObjectGioiTinh>()
            {
                new ObjectGioiTinh(){ GioiTinh = "Nam"},
                new ObjectGioiTinh(){ GioiTinh = "Nữ"},
            };

            Random rand = new Random();
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            //List nhân viên
            NhanVienList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);

            //TẠO ID NHÂN VIÊN
            var IDnew = createIdNV(IntroIDNV);
            Id = IDnew;

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

            DangKy = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                FormDangKy formDangKy = new FormDangKy();
                formDangKy.ShowDialog();
            });

            ChooseImages = new RelayCommand<Window>((p) => { return true; }, (p) => {
                OpenFileDialog openfiledialog = new OpenFileDialog();
                openfiledialog.Filter = openfiledialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
                if (openfiledialog.ShowDialog() == true)
                {
                    Images = new BitmapImage(new Uri(openfiledialog.FileName));
                    nameImages = openfiledialog.SafeFileName;

                    //location Images
                    dialog = openfiledialog.FileName;

                    fileiamge = "/Anh/" + nameImages;
                    addimages = "\\Anh\\" + nameImages;
                }
            });

            DangKyNhanVien = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //Thêm ảnh vào folder ảnh
                //Copy file anh vao folder Anh
                try
                {
                    File.Copy(dialog, directory + addimages);
                }
                catch { }
                //Thêm nhân viên
                var bm = new BitmapImage(new Uri(directory + this.fileiamge));
                var nv = new NhanVien() { Id = this.Id, TenNV = this.TenNV, GioiTinh = this.GioiTinh, NgaySinh = this.NgaySinh, DiaChi = this.DiaChi, SDT = this.SDT, Email = this.Email, Luong = 0, Anh = fileiamge, Url = bm };

                DataProvider.Ins.DB.NhanViens.Add(nv);
                MessageBox.Show($"Đăng ký thành công Username của bạn sẽ là: {Id}", "Thông báo");
                NhanVienList.Add(nv);

                //Thêm tài khoản
                string passEndCode = MD5Hash(Base64Encode(PasswordDangKy));
                var tk = new TaiKhoan() { IdNV = this.Id, Username = Id, Password = passEndCode, IdLoaiTK = 2 };
                DataProvider.Ins.DB.TaiKhoans.Add(tk);
                DataProvider.Ins.DB.SaveChanges();
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
                LoadNV.IdNV = LG.NhanVien.Id;
                LoadNV.TenNV = LG.NhanVien.TenNV;
                LoadNV.GioiTinh = LG.NhanVien.GioiTinh;
                LoadNV.NgaySinh = LG.NhanVien.NgaySinh;
                LoadNV.SDT = LG.NhanVien.SDT;
                LoadNV.DiaChi = LG.NhanVien.DiaChi;
                LoadNV.Email = LG.NhanVien.Email;
                LoadNV.Luong = LG.NhanVien.Luong;
                LoadNV.Anh = LG.NhanVien.Anh;
                LoadNV.Quyen = LG.LoaiTK.TenLoaiTK;
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

        string createIdNV(string valueID)
        {
            int STT = 0;
            string tmp = "NVSJDI_";
            valueID += "001";
            if (NhanVienList.Count() == 0 || NhanVienList == null)
            {
                return valueID;
            }
            else
            {
                foreach (var item in NhanVienList)
                {
                    while (item.Id == valueID)
                    {
                        tmp = "NVSJDI_";
                        STT = STT + 1;
                        string id = "000" + STT;
                        id = id.Substring(id.Length - 3, 3);
                        valueID = tmp + id;
                    }
                }
            }
            return valueID;
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
