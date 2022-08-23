using Microsoft.Win32;
using QuanLyCuaHang.Form;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class SettingViewModel:BaseViewModel
    {
        string directory;
        string fileiamge = null;
        string dialog;
        private string nameImages;
        public string addimages;
        private string _stringOldPass { get; set; }
        public string stringOldPass { get => _stringOldPass; set { _stringOldPass = value; OnPropertyChanged(); } }
        private string _stringNewPass { get; set; }
        public string stringNewPass { get => _stringNewPass; set { _stringNewPass = value; OnPropertyChanged(); } }
        private string _stringConFimPass { get; set; }
        public string stringConFimPass { get => _stringConFimPass; set { _stringConFimPass = value; OnPropertyChanged(); } }
        private string _Anh { get; set; }
        public string Anh { get => _Anh; set { _Anh = value; OnPropertyChanged(); } }
        private string _IdNV { get; set; }
        public string IdNV { get => _IdNV; set { _IdNV = value; OnPropertyChanged(); } }
        private string _TenNV { get; set; }
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
        private Nullable<double> _Luong;
        public Nullable<double> Luong { get => _Luong; set { _Luong = value; OnPropertyChanged(); } }
        private ObservableCollection<ObjectGioiTinh> _ListGioiTinh;
        public ObservableCollection<ObjectGioiTinh> ListGioiTinh { get => _ListGioiTinh; set { _ListGioiTinh = value; OnPropertyChanged(); } }
        public ICommand ChooseImages { get; set; }
        public ICommand EditNhanVien { get; set; }
        public ICommand MatKhauCu { get; set; }
        public ICommand MatKhauMoi { get; set; }
        public ICommand MatKhauXacNhan { get; set; }
        public ICommand LuuMatKhau { get; set; }
        public ICommand FormDoiMatKhauCommand { get; set; }
        public SettingViewModel()
        {
            ListGioiTinh = new ObservableCollection<ObjectGioiTinh>()
            {
               new ObjectGioiTinh(){GioiTinh = "Nam"},
               new ObjectGioiTinh(){GioiTinh = "Nữ"},
            };

            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

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

            EditNhanVien = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var CapNhatNV = DataProvider.Ins.DB.NhanViens.Where(x => x.Id == IdNV).SingleOrDefault();
                CapNhatNV.TenNV = TenNV;
                CapNhatNV.GioiTinh = GioiTinh;
                CapNhatNV.NgaySinh = NgaySinh;
                CapNhatNV.DiaChi = DiaChi;
                CapNhatNV.SDT = SDT;
                CapNhatNV.Email = Email;
                if (fileiamge != null)
                {
                    CapNhatNV.Anh = fileiamge;
                    CapNhatNV.Url = new BitmapImage(new Uri(directory + fileiamge));
                }
                DataProvider.Ins.DB.SaveChanges();
            });

            FormDoiMatKhauCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                FormDoiMatKhau formDoiMatKhau = new FormDoiMatKhau();
                formDoiMatKhau.ShowDialog();
            });

            MatKhauCu = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.stringOldPass = p.Password.ToString();
            });

            MatKhauMoi = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.stringNewPass = p.Password.ToString();
            });

            MatKhauXacNhan = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.stringConFimPass = p.Password.ToString();
            });

            LuuMatKhau = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string pass = MD5Hash(Base64Encode(stringOldPass));
                var kiemtraMatKhau = DataProvider.Ins.DB.TaiKhoans.Where(x=>x.Username == IdNV && x.Password == pass).SingleOrDefault();
                if (kiemtraMatKhau != null)
                {
                    if (stringNewPass == stringConFimPass)
                    {
                        string newpass = MD5Hash(Base64Encode(stringConFimPass));
                        kiemtraMatKhau.Password = newpass;
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Xác nhận mật khẩu mới chưa đúng", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu cũ không đúng vui lòng nhập lại !", "Thông báo");
                }
            });
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
