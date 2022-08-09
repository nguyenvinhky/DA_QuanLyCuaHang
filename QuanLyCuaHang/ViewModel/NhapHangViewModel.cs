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
    public class NhapHangViewModel : BaseViewModel
    {
        private string directory = "";
        private string MaHDN = "";
        private double gn;
        private int sln;
        private double _TongTien { get; set; }
        public double TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private string _IdTTHDN { get; set; }
        public string IdTTHDN { get => _IdTTHDN; set { _IdTTHDN = value; OnPropertyChanged(); } }
        private double _GiaNhap { get; set; }
        public double GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }
        private int _SoLuongNhap { get; set; }
        public int SoLuongNhap { get => _SoLuongNhap; set { _SoLuongNhap = value; OnPropertyChanged(); } }
        private string _SLT { get; set; }
        public string SLT { get => _SLT; set { _SLT = value; OnPropertyChanged(); } }
        private ObservableCollection<Model.HDN> _ListHDN { get; set; }
        public ObservableCollection<Model.HDN> ListHDN { get => _ListHDN; set { _ListHDN = value; OnPropertyChanged(); } }
        private ObservableCollection<SanPham> _ListCBB_SP { get; set; }
        public ObservableCollection<SanPham> ListCBB_SP { get => _ListCBB_SP; set { _ListCBB_SP = value; OnPropertyChanged(); } }
        private ObservableCollection<Model.Size> _ListCBB_Size { get; set; }
        public ObservableCollection<Model.Size> ListCBB_Size { get => _ListCBB_Size; set { _ListCBB_Size = value; OnPropertyChanged(); } }
        private ObservableCollection<Model.TTHDN> _ListTTHDN { get; set; }
        public ObservableCollection<Model.TTHDN> ListTTHDN { get => _ListTTHDN; set { _ListTTHDN = value; OnPropertyChanged(); } }
        private ObservableCollection<Model.NCC> _ListCBB_NCC { get; set; }
        public ObservableCollection<Model.NCC> ListCBB_NCC { get => _ListCBB_NCC; set { _ListCBB_NCC = value; OnPropertyChanged(); } }
        private Model.TonKho _ListTonKho { get; set; }
        public Model.TonKho ListTonKho { get => _ListTonKho; set { _ListTonKho = value; OnPropertyChanged(); } }
        private SanPham _SelectedItemSP { get; set; }
        public SanPham SelectedItemSP 
        { 
            get => _SelectedItemSP;
            set 
            { 
                _SelectedItemSP = value;
                OnPropertyChanged();
                if (SelectedItemSP == null)
                    return;
                ListCBB_Size = new ObservableCollection<Model.Size>(DataProvider.Ins.DB.Sizes.Where(x=>x.IdLSP == SelectedItemSP.LoaiSanPham.Id));
            } 
        }

        private Model.Size _SelectedItemSize { get; set; }
        public Model.Size SelectedItemSize
        {
            get => _SelectedItemSize;
            set
            {
                _SelectedItemSize = value;
                OnPropertyChanged();
                if (SelectedItemSize == null)
                    return;
                ListTonKho = DataProvider.Ins.DB.TonKhoes.Where(x => x.IdSP == SelectedItemSP.Id && x.IdSize == SelectedItemSize.Id).SingleOrDefault();
                if (ListTonKho == null)
                {
                    SLT = "SẢN PHẨM ĐÃ HÊT";
                }
                else
                    SLT = ListTonKho.SLTon.ToString();
            }
        }

        private NCC _SelectedItemNCC { get; set; }
        public NCC SelectedItemNCC
        {
            get => _SelectedItemNCC;
            set
            {
                _SelectedItemNCC = value;
                OnPropertyChanged();
                if (SelectedItemNCC == null)
                    return;
            }
        }

        private TTHDN _SelectedItemTTHDN { get; set; }
        public TTHDN SelectedItemTTHDN
        {
            get => _SelectedItemTTHDN;
            set
            {
                _SelectedItemTTHDN = value;
                OnPropertyChanged();
                if (SelectedItemTTHDN == null)
                    return;
                gn = (double)SelectedItemTTHDN.GiaNhap;
                sln = (int)SelectedItemTTHDN.SLNhap;
            }
        }

        private HDN _SelectedItemHDN { get; set; }
        public HDN SelectedItemHDN
        {
            get => _SelectedItemHDN;
            set
            {
                _SelectedItemHDN = value;
                OnPropertyChanged();
                if (_SelectedItemHDN == null)
                    return;
                FormCTHD cthd = new FormCTHD();
                ListTTHDN = new ObservableCollection<TTHDN>(DataProvider.Ins.DB.TTHDNs.Where(x => x.IdHDN == SelectedItemHDN.Id));
                cthd.ShowDialog();
            }
        }
        public ICommand CreateHDN { get; set; }
        public ICommand AddTTHDN { get; set; }
        public ICommand DeleteTTHDN { get; set; }
        public ICommand HuyTTHDN { get; set; }
        public ICommand ThanhToanTTHDN { get; set; }
        public ICommand XoaPhieuHDN { get; set; }
        public NhapHangViewModel()
        {
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //Danh sách hóa đơn nhập
            ListHDN = new ObservableCollection<Model.HDN>(DataProvider.Ins.DB.HDNs);

            //Danh sách combobox sản phẩm
            ListCBB_SP = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoadUrl();

            //Danh sách combobox nhà cung cấp
            ListCBB_NCC = new ObservableCollection<NCC>(DataProvider.Ins.DB.NCCs);

            //List thông tin hóa đơn nhập
            ListTTHDN = new ObservableCollection<TTHDN>(DataProvider.Ins.DB.TTHDNs);

            CreateHDN = new RelayCommand<Window>((p)=> 
            { 
                return true;
            }, (p)=> 
            {
                IdTTHDN = $"TTCTHDN_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";
                MaHDN = $"HDN_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";
                var hdn = new HDN() { Id = MaHDN, NgayNhap = DateTime.Now, TongTienHDN = 0 };
                DataProvider.Ins.DB.HDNs.Add(hdn);
                DataProvider.Ins.DB.SaveChanges();
                ListHDN.Add(hdn);

                FormThemHoaDon addHD = new FormThemHoaDon();
                ListTTHDN = new ObservableCollection<TTHDN>(DataProvider.Ins.DB.TTHDNs.Where(x => x.IdHDN == MaHDN));
                
                SLT = 0 + "";
                //Giá trị tổng tiền
                TongTien = 0;
                addHD.ShowDialog();
            });

            AddTTHDN = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                IdTTHDN = $"TTCTHDN_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";
                var tthdn = new TTHDN() { Id = IdTTHDN, IdSP = SelectedItemSP.Id, IdNCC = SelectedItemNCC.Id, IdSize = SelectedItemSize.Id, IdHDN = MaHDN, GiaNhap = GiaNhap, SLNhap = SoLuongNhap };
                DataProvider.Ins.DB.TTHDNs.Add(tthdn);
                DataProvider.Ins.DB.SaveChanges();
                ListTTHDN.Add(tthdn);

                //Tính tổng tiền
                TongTien = TongTien + GiaNhap * SoLuongNhap;

                //Thêm số lượng sản phẩm theo mã và size
                if (ListTonKho == null)
                {
                    var objectTK = new TonKho() { IdSP = SelectedItemSP.Id, IdSize = SelectedItemSize.Id, SLTon = SoLuongNhap };
                    DataProvider.Ins.DB.TonKhoes.Add(objectTK);
                    DataProvider.Ins.DB.SaveChanges();
                    SLT = SoLuongNhap + "";
                    ListTonKho = DataProvider.Ins.DB.TonKhoes.Where(x => x.IdSP == SelectedItemSP.Id && x.IdSize == SelectedItemSize.Id).SingleOrDefault();
                }
                else
                {
                    ListTonKho.SLTon += SoLuongNhap;
                    DataProvider.Ins.DB.SaveChanges();
                    SLT = ListTonKho.SLTon + ""; 
                }

            });

            DeleteTTHDN = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                //Giảm số lượng sản phẩm theo mã và size
                var updateSLT = DataProvider.Ins.DB.TonKhoes.Where(x => x.IdSP == SelectedItemTTHDN.IdSP && x.IdSize == SelectedItemTTHDN.IdSize).SingleOrDefault();
                updateSLT.SLTon -= SelectedItemTTHDN.SLNhap;
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.TTHDNs.Remove(SelectedItemTTHDN);
                DataProvider.Ins.DB.SaveChanges();
                ListTTHDN.Remove(SelectedItemTTHDN);

                //Trừ tiền khi xóa sản phẩm nhập
                TongTien = TongTien - gn * sln;
            });

            HuyTTHDN = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn hủy hóa đơn {MaHDN} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                DataProvider.Ins.DB.TTHDNs.RemoveRange(ListTTHDN);
                DataProvider.Ins.DB.SaveChanges();

                //Lấy đối tượng hóa đơn hiện tại
                var objecthdn = DataProvider.Ins.DB.HDNs.Where(x=>x.Id == MaHDN).SingleOrDefault();
                DataProvider.Ins.DB.HDNs.Remove(objecthdn);
                DataProvider.Ins.DB.SaveChanges();
                ListHDN.Remove(objecthdn);
                p.Close();
            });

            ThanhToanTTHDN = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn thanh toán hóa đơn {MaHDN} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                var thanhtoan = DataProvider.Ins.DB.HDNs.Where(x => x.Id == MaHDN).SingleOrDefault();
                thanhtoan.TongTienHDN = TongTien;
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });

            XoaPhieuHDN = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa phiếu {SelectedItemHDN.Id} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                DataProvider.Ins.DB.TTHDNs.RemoveRange(ListTTHDN);
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.HDNs.Remove(SelectedItemHDN);
                DataProvider.Ins.DB.SaveChanges();
                ListHDN.Remove(SelectedItemHDN);

                p.Close();
            });
        }

        void LoadUrl()
        {
            if (ListCBB_SP == null)
                return;
            foreach (SanPham i in ListCBB_SP)
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
