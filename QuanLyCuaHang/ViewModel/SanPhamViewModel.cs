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
using System.Windows.Media.Imaging;

namespace QuanLyCuaHang.ViewModel
{
    public class SanPhamViewModel : BaseViewModel
    {
        string directory = "";
        string nameImages = "";
        string dialog = "";
        string fileiamge = "";
        string addimages = "";
        int Select_ID_LSP;
        private string _Id { get; set; }
        public string Id { get => _Id; set { _Id = value; ; OnPropertyChanged(); } }
        private string _Anh { get; set; }
        public string Anh { get => _Anh; set { _Anh = value; ; OnPropertyChanged(); } }
        private string _TenSP { get; set; }
        public string TenSP { get => _TenSP; set { _TenSP = value; ; OnPropertyChanged(); } }
        private float _GiaBan { get; set; }
        public float GiaBan { get => _GiaBan; set { _GiaBan = value; ; OnPropertyChanged(); } }
        private string _GhiChu { get; set; }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; ; OnPropertyChanged(); } }
        private BitmapImage _ImagesSP { get; set; }
        public BitmapImage ImagesSP { get => _ImagesSP; set { _ImagesSP = value; ; OnPropertyChanged(); } }
        private ObservableCollection<SanPham> _ListSanPham;
        public ObservableCollection<SanPham> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiSanPham> _ListCBB_LSP;
        public ObservableCollection<LoaiSanPham> ListCBB_LSP { get => _ListCBB_LSP; set { _ListCBB_LSP = value; OnPropertyChanged(); } }
        private LoaiSanPham _SelectedValue_LSP;
        public LoaiSanPham SelectedValue_LSP 
        { 
            get => _SelectedValue_LSP; 
            set 
            { 
                _SelectedValue_LSP = value; 
                OnPropertyChanged();
                Select_ID_LSP = SelectedValue_LSP.Id;
            } 
        }

        public ICommand AddSanPham { get; set; }
        public ICommand OpenFormAddSanPham { get; set; }
        public ICommand ChooseImages { get; set; }
        public SanPhamViewModel()
        {
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //Danh sách sản phẩm
            ListSanPham = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoadUrl();

            //Danh sách combobox loại sản phẩm
            ListCBB_LSP = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);

            OpenFormAddSanPham = new RelayCommand<Window>((p) => { return true; }, (p) => 
            {
                FormThemSanPham fAddSP = new FormThemSanPham();
                fAddSP.ShowDialog();
            });

            ChooseImages = new RelayCommand<Window>((p) => { return true; }, (p) => {
                OpenFileDialog openfiledialog = new OpenFileDialog();
                openfiledialog.Filter = openfiledialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
                if (openfiledialog.ShowDialog() == true)
                {
                    ImagesSP = new BitmapImage(new Uri(openfiledialog.FileName));
                    nameImages = openfiledialog.SafeFileName;

                    //location Images
                    dialog = openfiledialog.FileName;

                    fileiamge = "/Anh/" + nameImages;
                    addimages = "\\Anh\\" + nameImages;
                }
            });

            AddSanPham = new RelayCommand<Window>((p) =>
            {
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
                //Thêm sản phẩm
                var bm = new BitmapImage(new Uri(directory + this.fileiamge));
                var sp = new SanPham() { Id = Id, Anh = fileiamge, TenSP = TenSP, IdLoaiSP = SelectedValue_LSP.Id, GiaBan = GiaBan, GhiChu = GhiChu, Url = bm};

                DataProvider.Ins.DB.SanPhams.Add(sp);
                MessageBox.Show($"Thêm sản phẩm {TenSP} thành công", "Thông báo");
                DataProvider.Ins.DB.SaveChanges();
                ListSanPham.Add(sp);
            });
        }
        void LoadUrl()
        {
            if (ListSanPham == null)
                return;
            foreach (SanPham i in ListSanPham)
            {
                try
                {
                    i.Url = new BitmapImage(new Uri(directory + i.Anh));
                }
                catch { }
            }
        }
    }
}
