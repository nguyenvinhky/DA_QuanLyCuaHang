using Microsoft.Win32;
using QuanLyCuaHang.Form;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        string directory;
        string fileiamge = "";
        string dialog;
        private string nameImages;
        private string _Id;
        public string Id { get => _Id; set => _Id = value; }
        private BitmapImage _Images;
        public BitmapImage Images { get => _Images; set { _Images = value; OnPropertyChanged(); } }
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
                ttnv.ShowDialog();
            }
        }
        private ObservableCollection<NhanVien> _NhanVienList;
        public ObservableCollection<NhanVien> NhanVienList { get => _NhanVienList; set { _NhanVienList = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiTK> _CBBLoaiTKList;
        public ObservableCollection<LoaiTK> CBBLoaiTKList { get => _CBBLoaiTKList; set { _CBBLoaiTKList = value; OnPropertyChanged(); } }

        public ICommand OpenAddNhanVien { get; set; }
        public ICommand ChooseImages { get; set; }

        public NhanVienViewModel()
        {
            NhanVienList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            CBBLoaiTKList = new ObservableCollection<LoaiTK>(DataProvider.Ins.DB.LoaiTKs);
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            OpenAddNhanVien = new RelayCommand<Window>((p) => { return true; }, (p) => {
                FormThemNhanVien fAddNV = new FormThemNhanVien();
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

                    fileiamge = "\\Anh\\" + nameImages;

                    //Copy file anh vao folder Anh
                    //try
                    //{
                    //    File.Copy(dialog, directory + fileiamge);
                    //}
                    //catch { }
                }
            });
        }
    }
}
