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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLyCuaHang.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        string IntroIDNV = "NVSJDI_";
        string directory;
        string fileiamge = null;
        string dialog;
        private string nameImages;
        public string addimages;
        private int _IdLTK;
        private string CapNhatImages;
        public int IdLTK { get => _IdLTK; set { _IdLTK = value; OnPropertyChanged(); } }
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
        private Nullable<double> _Luong;
        public Nullable<double> Luong { get => _Luong; set { _Luong = value; OnPropertyChanged(); } }
        private BitmapImage _Images;
        public BitmapImage Images { get => _Images; set { _Images = value; OnPropertyChanged(); } }
        private string _LTK;
        public string LTK { get => _LTK; set { _LTK = value; OnPropertyChanged(); } }
        private ObservableCollection<TaiKhoan> _ListTaiKhoan;
        public ObservableCollection<TaiKhoan> ListTaiKhoan { get => _ListTaiKhoan; set { _ListTaiKhoan = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiTK> _CBBLoaiTKList;
        public ObservableCollection<LoaiTK> CBBLoaiTKList { get => _CBBLoaiTKList; set { _CBBLoaiTKList = value; OnPropertyChanged(); } }

        private LoaiTK _SelectedValueCBBLTK;
        public LoaiTK SelectedValueCBBLTK
        {
            get => _SelectedValueCBBLTK;
            set
            {
                _SelectedValueCBBLTK = value;
                if (SelectedValueCBBLTK == null)
                    return;
                IdLTK = SelectedValueCBBLTK.Id;
            }
        }
        private TaiKhoan getidtk;
        private NhanVien _SelectedNhanVien; 
        public NhanVien SelectedNhanVien 
        { 
            get => _SelectedNhanVien;
            set 
            { 
                _SelectedNhanVien = value;
                if (SelectedNhanVien == null)
                    return;
                FormThongTinNhanVien ttnv = new FormThongTinNhanVien();
                getidtk = DataProvider.Ins.DB.TaiKhoans.Where(x => x.IdNV == SelectedNhanVien.Id).SingleOrDefault();
                var getidltk = DataProvider.Ins.DB.LoaiTKs.Where(x => x.Id == getidtk.IdLoaiTK).SingleOrDefault();
                LTK = getidltk.TenLoaiTK;
                Id = SelectedNhanVien.Id;
                TenNV = SelectedNhanVien.TenNV;
                GioiTinh = SelectedNhanVien.GioiTinh;
                NgaySinh = SelectedNhanVien.NgaySinh;
                DiaChi = SelectedNhanVien.DiaChi;
                SDT = SelectedNhanVien.SDT;
                Email = SelectedNhanVien.Email;
                Luong = SelectedNhanVien.Luong;
                LoadUrl();
                Images = SelectedNhanVien.Url;
                CapNhatImages = SelectedNhanVien.Anh;
                ttnv.ShowDialog();
            }
        }
        private ObservableCollection<NhanVien> _NhanVienList;
        public ObservableCollection<NhanVien> NhanVienList { get => _NhanVienList; set { _NhanVienList = value; OnPropertyChanged(); } }

        public ICommand OpenAddNhanVien { get; set; }
        public ICommand ChooseImages { get; set; }
        public ICommand AddNhanVien { get; set; }
        public ICommand DeleteNhanVien { get; set; }
        public ICommand EditNhanVien { get; set; }
        public ICommand ResetPassword { get; set; }
        public ICommand HuyClick { get; set; }
        public NhanVienViewModel()
        {
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //List nhân viên
            NhanVienList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            LoadUrl();
            //Create mã nhân viên
            //Combobox loại tài khoản
            CBBLoaiTKList = new ObservableCollection<LoaiTK>(DataProvider.Ins.DB.LoaiTKs);
            //List tài khoản
            ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);

            ResetPassword = new RelayCommand<Window>((p) => { return true; }, (p) => {
                getidtk.Password = MD5Hash(Base64Encode("123"));
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã cập nhật mật khẩu về 123", "Thông báo");
            });

            HuyClick = new RelayCommand<Window>((p) => 
            {
                if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(TenNV) && string.IsNullOrEmpty(GioiTinh) && string.IsNullOrEmpty(DiaChi) && string.IsNullOrEmpty(SDT) && string.IsNullOrEmpty(Email) && Luong == 0 && Images == null)
                {
                    return false;
                }
                return true; 
            }, (p) => 
            {
                FormThemNhanVien fAddNV = new FormThemNhanVien();
                Id = "";
                TenNV = "";
                GioiTinh = "";
                NgaySinh = DateTime.Now.Date;
                DiaChi = "";
                SDT = "";
                Email = "";
                Luong = 0;
                Images = null;
                fAddNV.ShowDialog();
            });

            OpenAddNhanVien = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var IDnew = createIdNV(IntroIDNV);
                FormThemNhanVien fAddNV = new FormThemNhanVien();
                Id = IDnew;
                TenNV = "";
                GioiTinh = "";
                NgaySinh = DateTime.Now.Date;
                DiaChi = "";
                SDT = "";
                Email = "";
                Luong = 0;
                Images = null;
                fAddNV.ShowDialog();
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

            AddNhanVien = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(TenNV) || string.IsNullOrEmpty(GioiTinh) || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(SDT))
                {
                    return false;
                }

                var listIdNhanVien = DataProvider.Ins.DB.NhanViens.Where(n => n.Id == Id);
                if (listIdNhanVien == null || listIdNhanVien.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
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
                var nv = new NhanVien() { Id = this.Id, TenNV = this.TenNV, GioiTinh = this.GioiTinh, NgaySinh = this.NgaySinh, DiaChi = this.DiaChi, SDT = this.SDT, Email = this.Email, Luong = this.Luong, Anh = fileiamge, Url = bm };

                DataProvider.Ins.DB.NhanViens.Add(nv);
                MessageBox.Show($"Thêm nhân viên {TenNV} thành công", "Thông báo");
                NhanVienList.Add(nv);

                //Thêm tài khoản
                string passEndCode = MD5Hash(Base64Encode(Id));
                var tk = new TaiKhoan() { IdNV = this.Id, Username = Id, Password = passEndCode, IdLoaiTK = IdLTK };
                DataProvider.Ins.DB.TaiKhoans.Add(tk);
                DataProvider.Ins.DB.SaveChanges();

                var IDnew = createIdNV(IntroIDNV);
                Id = IDnew;
            });

            EditNhanVien = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            { 
                var CapNhatNV = DataProvider.Ins.DB.NhanViens.Where(x => x.Id == SelectedNhanVien.Id).SingleOrDefault();
                CapNhatNV.TenNV = TenNV;
                CapNhatNV.GioiTinh = GioiTinh;
                CapNhatNV.NgaySinh = NgaySinh;
                CapNhatNV.DiaChi = DiaChi;
                CapNhatNV.SDT = SDT;
                CapNhatNV.Email = Email;
                CapNhatNV.Luong = Luong;
                if (fileiamge!=null)
                    CapNhatNV.Anh = fileiamge;
                else
                    CapNhatNV.Anh = SelectedNhanVien.Anh;
                CapNhatNV.Url = new BitmapImage(new Uri(directory+ fileiamge));
                getidtk.IdLoaiTK = IdLTK;
                DataProvider.Ins.DB.SaveChanges();                
            });


            DeleteNhanVien = new RelayCommand<Window>((p) => 
            { 
                return true; 
            }, (p) => 
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {SelectedNhanVien.TenNV} không ???", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //xoa taikhoan
                    var tk = DataProvider.Ins.DB.TaiKhoans.Where(t => t.IdNV == SelectedNhanVien.Id).SingleOrDefault();
                    DataProvider.Ins.DB.TaiKhoans.Remove(tk);
                    DataProvider.Ins.DB.SaveChanges();


                    //xoa nhanvien
                    DataProvider.Ins.DB.NhanViens.Remove(SelectedNhanVien);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show($"Xóa nhân viên {SelectedNhanVien.TenNV} thành công", "Thông báo");
                    //Load list nhân viên
                    NhanVienList.Remove(SelectedNhanVien);
                    p.Close();
                }
                else
                    return;
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
        void LoadUrl()
        {
            if (NhanVienList == null)
                return;
            foreach (NhanVien i in NhanVienList)
            {
                try
                {
                    i.Url = new BitmapImage(new Uri(directory + i.Anh));
                }
                catch { }
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
    }
}
