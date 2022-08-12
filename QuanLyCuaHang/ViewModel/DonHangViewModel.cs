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
    public class DonHangViewModel:BaseViewModel
    {
        private string directory = "";
        private double gb;
        private int slb;
        private string _SLT { get; set; }
        public string SLT { get => _SLT; set { _SLT = value; OnPropertyChanged(); } }
        private double _GiaBan { get; set; }
        public double GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }
        private double _TongTien { get; set; }
        public double TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private string _MaTTHDB { get; set; }
        public string MaTTHDB { get => _MaTTHDB; set { _MaTTHDB = value; OnPropertyChanged(); } }
        private int _SoLuongMua { get; set; }
        public int SoLuongMua { get => _SoLuongMua; set { _SoLuongMua = value; OnPropertyChanged(); } }
        private string _IdNV { get; set; }
        public string IdNV { get => _IdNV; set { _IdNV = value; OnPropertyChanged(); } }
        private string _TenNV { get; set; }
        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }
        private string _MaHDB { get; set; }
        public string MaHDB { get => _MaHDB; set { _MaHDB = value; OnPropertyChanged(); } }
        private DateTime _NgayLapHoaDon { get; set; }
        public DateTime NgayLapHoaDon { get => _NgayLapHoaDon; set { _NgayLapHoaDon = value; OnPropertyChanged(); } }
        private ObservableCollection<KhachHang> _ListCBBKhachHang;
        public ObservableCollection<KhachHang> ListCBBKhachHang { get => _ListCBBKhachHang; set { _ListCBBKhachHang = value; OnPropertyChanged(); } }
        private ObservableCollection<HDB> _ListHDB;
        public ObservableCollection<HDB> ListHDB { get => _ListHDB; set { _ListHDB = value; OnPropertyChanged(); } }
        private HDB _SelectedItemHDB { get; set; }
        public HDB SelectedItemHDB
        {
            get => _SelectedItemHDB;
            set
            {
                _SelectedItemHDB = value;
                OnPropertyChanged();
                if (SelectedItemHDB == null)
                    return;
                FormCTHDB cthdb = new FormCTHDB();
                ListTTHDB = new ObservableCollection<TTHDB>(DataProvider.Ins.DB.TTHDBs.Where(x => x.IdHDB == SelectedItemHDB.Id));
                cthdb.ShowDialog();
            }
        }
        private Model.TonKho _ListTonKho { get; set; }
        public Model.TonKho ListTonKho { get => _ListTonKho; set { _ListTonKho = value; OnPropertyChanged(); } }
        private KhachHang _SelectedCBBKH;
        public KhachHang SelectedCBBKH 
        { 
            get => _SelectedCBBKH; 
            set 
            { 
                _SelectedCBBKH = value; 
                OnPropertyChanged();
                if (SelectedCBBKH == null)
                    return;
            } 
        }
        private ObservableCollection<SanPham> _ListCBB_SP { get; set; }
        public ObservableCollection<SanPham> ListCBB_SP { get => _ListCBB_SP; set { _ListCBB_SP = value; OnPropertyChanged(); } }
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
                ListCBB_Size = new ObservableCollection<Model.Size>(DataProvider.Ins.DB.Sizes.Where(x => x.IdLSP == SelectedItemSP.LoaiSanPham.Id));
                GiaBan = (double)SelectedItemSP.GiaBan;
            }
        }
        private ObservableCollection<Model.Size> _ListCBB_Size { get; set; }
        public ObservableCollection<Model.Size> ListCBB_Size { get => _ListCBB_Size; set { _ListCBB_Size = value; OnPropertyChanged(); } }
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
        private ObservableCollection<Model.TTHDB> _ListTTHDB { get; set; }
        public ObservableCollection<Model.TTHDB> ListTTHDB { get => _ListTTHDB; set { _ListTTHDB = value; OnPropertyChanged(); } }
        private TTHDB _SelectedItemTTHDB { get; set; }
        public TTHDB SelectedItemTTHDB
        {
            get => _SelectedItemTTHDB;
            set
            {
                _SelectedItemTTHDB = value;
                OnPropertyChanged();
                if (SelectedItemTTHDB == null)
                    return;
                gb = (double)SelectedItemTTHDB.SanPham.GiaBan;
                slb = (int)SelectedItemTTHDB.SL;
            }
        }
        public ICommand CreateHDB { get; set; }
        public ICommand CreateHDBClick { get; set; }
        public ICommand AddTTHDB { get; set; }
        public ICommand DeleteTTHDB { get; set; }
        public ICommand HuyHDB { get; set; }
        public ICommand ThanhToanTTHDB { get; set; }
        public ICommand XoaPhieuHDB { get; set; }
        public DonHangViewModel()
        {
            //Lấy đường dẫn nơi chứa Project
            directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            //Danh sách hóa đơn bán
            ListHDB = new ObservableCollection<HDB>(DataProvider.Ins.DB.HDBs);

            //Danh sach combobox khách hàng
            ListCBBKhachHang = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);

            //Danh sách combobox sản phẩm
            ListCBB_SP = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoadUrl();

            CreateHDB = new RelayCommand<Window>((p) => 
            {
                return true; 
            }, (p) => 
            {
                NgayLapHoaDon = DateTime.Now;
                MaHDB = $"HDB_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";
                FormCreateHDB formCreateHDB = new FormCreateHDB();
                formCreateHDB.ShowDialog();
            });

            CreateHDBClick = new RelayCommand<Window>((p) =>
            {
                if (SelectedCBBKH == null)
                    return false;
                return true;
            }, (p) =>
            {
                var hdb = new HDB() { Id = MaHDB, IdNV = IdNV, IdKH = SelectedCBBKH.Id, NgayBan = NgayLapHoaDon, TongTien = 0};

                DataProvider.Ins.DB.HDBs.Add(hdb);
                DataProvider.Ins.DB.SaveChanges();
                ListHDB.Add(hdb);

                //Đóng form tạo hóa đơn
                p.Close();


                MaTTHDB = $"TTCTHDN_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";
                FormThemHoaDonBan formThemHoaDonBan = new FormThemHoaDonBan();
                TongTien = 0;
                MessageBox.Show($"Đã tạo phiếu bán hàng {MaHDB}", "Thông báo");
                ListTTHDB = new ObservableCollection<Model.TTHDB>(DataProvider.Ins.DB.TTHDBs.Where(x=>x.Id == MaHDB));
                formThemHoaDonBan.ShowDialog();
            });

            AddTTHDB = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (SoLuongMua <= ListTonKho.SLTon)
                {
                    var tthdb = new TTHDB() { Id = MaTTHDB, IdSP = SelectedItemSP.Id, IdSize = SelectedItemSize.Id, IdHDB = MaHDB, SL = SoLuongMua };

                    DataProvider.Ins.DB.TTHDBs.Add(tthdb);
                    DataProvider.Ins.DB.SaveChanges();
                    ListTTHDB.Add(tthdb);

                    //Tạo mã TTHDB mới
                    MaTTHDB = $"TTCTHDN_{DateTime.Now.ToString("ddMMyyyy_HHmmssff")}";

                    //Tính sô lượng tồn kho
                    ListTonKho.SLTon -= SoLuongMua;
                    DataProvider.Ins.DB.SaveChanges();
                    SLT = ListTonKho.SLTon + "";

                    //Tính tổng tiền
                    TongTien = TongTien + GiaBan * SoLuongMua;
                }
                else
                {
                    MessageBox.Show("Số sản phẩm không đủ đáp ứng", "Thông báo");
                    return;
                }

            });

            DeleteTTHDB = new RelayCommand<Window>((p) =>
            {
                if (SelectedItemTTHDB == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //Trả lại số lượng sản phẩm theo mã và size
                var updateSLT = DataProvider.Ins.DB.TonKhoes.Where(x => x.IdSP == SelectedItemTTHDB.IdSP && x.IdSize == SelectedItemTTHDB.IdSize).SingleOrDefault();
                updateSLT.SLTon += SelectedItemTTHDB.SL;
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.TTHDBs.Remove(SelectedItemTTHDB);
                DataProvider.Ins.DB.SaveChanges();
                ListTTHDB.Remove(SelectedItemTTHDB);

                //Trừ tiền khi xóa sản phẩm nhập
                TongTien = TongTien - gb * slb;
            });

            ThanhToanTTHDB = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn thanh toán hóa đơn {MaHDB} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                var thanhtoan = DataProvider.Ins.DB.HDBs.Where(x => x.Id == MaHDB).SingleOrDefault();
                thanhtoan.TongTien = TongTien;
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });

            HuyHDB = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn hủy hóa đơn {MaHDB} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                foreach (TTHDB item in ListTTHDB)
                {
                    var updateSLT = DataProvider.Ins.DB.TonKhoes.Where(x=>x.IdSP == item.IdSP && x.IdSize == item.IdSize).SingleOrDefault();
                    updateSLT.SLTon += item.SL;
                    DataProvider.Ins.DB.TTHDBs.Remove(item);
                    DataProvider.Ins.DB.SaveChanges();
                }

                //Lấy đối tượng hóa đơn hiện tại
                var objecthdb = DataProvider.Ins.DB.HDBs.Where(x => x.Id == MaHDB).SingleOrDefault();
                DataProvider.Ins.DB.HDBs.Remove(objecthdb);
                DataProvider.Ins.DB.SaveChanges();
                ListHDB.Remove(objecthdb);
                p.Close();
            });

            XoaPhieuHDB = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa phiếu {SelectedItemHDB.Id} không ?", "Thông báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                DataProvider.Ins.DB.TTHDBs.RemoveRange(ListTTHDB);
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.HDBs.Remove(SelectedItemHDB);
                DataProvider.Ins.DB.SaveChanges();
                ListHDB.Remove(SelectedItemHDB);

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
