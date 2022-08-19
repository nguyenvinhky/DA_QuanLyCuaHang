using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    public class ThongKeViewModel:BaseViewModel
    {
        private MaterialDesignThemes.Wpf.PackIconKind _ChangesIcon;
        public MaterialDesignThemes.Wpf.PackIconKind ChangesIcon { get => _ChangesIcon; set { _ChangesIcon = value; OnPropertyChanged(); } }
        private DateTime _NgayDauThang;
        public DateTime NgayDauThang { get => _NgayDauThang; set { _NgayDauThang = value; OnPropertyChanged(); } }
        private DateTime _NgayHienTai;
        public DateTime NgayHienTai { get => _NgayHienTai; set { _NgayHienTai = value; OnPropertyChanged(); } }
        private SeriesCollection _PieChartSLSP;
        public SeriesCollection PieChartSLSP { get => _PieChartSLSP; set { _PieChartSLSP = value; OnPropertyChanged(); } }
        private SeriesCollection _PieChartTOP_SP;
        public SeriesCollection PieChartTOP_SP { get => _PieChartTOP_SP; set { _PieChartTOP_SP = value; OnPropertyChanged(); } }
        private SeriesCollection _LineChartChuKi;
        public SeriesCollection LineChartChuKi { get => _LineChartChuKi; set { _LineChartChuKi = value; OnPropertyChanged(); } }
        private string _IDKhachHangTiemNang;
        public string IDKhachHangTiemNang { get => _IDKhachHangTiemNang; set { _IDKhachHangTiemNang = value; OnPropertyChanged(); } }
        private string _KhachHangTiemNang;
        public string KhachHangTiemNang { get => _KhachHangTiemNang; set { _KhachHangTiemNang = value; OnPropertyChanged(); } }
        
        private double _DoanhThuVSThangTruoc;
        public double DoanhThuVSThangTruoc { get => _DoanhThuVSThangTruoc; set { _DoanhThuVSThangTruoc = value; OnPropertyChanged(); } }

        private string _IDNhanVienTiemNang;
        public string IDNhanVienTiemNang { get => _IDNhanVienTiemNang; set { _IDNhanVienTiemNang = value; OnPropertyChanged(); } }
        private string _NhanVienTiemNang;
        public string NhanVienTiemNang { get => _NhanVienTiemNang; set { _NhanVienTiemNang = value; OnPropertyChanged(); } }
        private ObservableCollection<ObjectChuKiDoanhThu> _ListChuKi { get; set; }
        public ObservableCollection<ObjectChuKiDoanhThu> ListChuKi { get => _ListChuKi; set { _ListChuKi = value; OnPropertyChanged(); } }
        public List<string> _Labels;
        public List<string> Labels { get => _Labels; set { _Labels = value; OnPropertyChanged(); } }
        public Func<double, string> _Formatter;
        public Func<double, string> Formatter { get => _Formatter; set { _Formatter = value; OnPropertyChanged(); } }
        private ObjectChuKiDoanhThu _SelectedChuKi { get; set; }
        public ObjectChuKiDoanhThu SelectedChuKi 
        { 
            get => _SelectedChuKi; 
            set 
            { 
                _SelectedChuKi = value; 
                OnPropertyChanged();
                if (SelectedChuKi == null)
                    return;
                SeriesCollection series = new SeriesCollection();
                if (SelectedChuKi.NameChuKi == "Tháng")
                {
                    var ChuKiThang = DataProvider.Ins.DB.HDBs.Where(x => x.NgayBan.Value.Year == DateTime.Now.Year).GroupBy(t => t.NgayBan.Value.Month).Select(x => new { TKMonth = x.Key, Sum = x.Sum(t => t.TongTien) }).OrderBy(t => t.TKMonth).ToList();
                    List<int> ArrayCKT = new List<int>();
                    List<string> ArrayNameCKT = new List<string>();
                    foreach (var item in ChuKiThang)
                    {
                        ArrayCKT.Add((int)item.Sum);
                        ArrayNameCKT.Add("Tháng " + item.TKMonth);
                    }
                    series.Add(new LineSeries() { Title = "Doanh Thu", Values = new ChartValues<int>(ArrayCKT) });
                    LineChartChuKi = series;
                    Labels = ArrayNameCKT;
                    Formatter = Values => Values.ToString("N") + " VNĐ";
                }
                else
                {
                    var ChuKiNam = DataProvider.Ins.DB.HDBs.GroupBy(t => t.NgayBan.Value.Year).Select(x => new { TKYear = x.Key, Sum = x.Sum(t => t.TongTien) }).OrderBy(t => t.TKYear).ToList();
                    List<int> ArrayCKN = new List<int>();
                    List<string> ArrayNameCKN = new List<string>();
                    foreach (var item in ChuKiNam)
                    {
                        ArrayCKN.Add((int)item.Sum);
                        ArrayNameCKN.Add("Năm " + item.TKYear);
                    }
                    series.Add(new LineSeries() { Title = "Doanh Thu", Values = new ChartValues<int>(ArrayCKN) });
                    LineChartChuKi = series;
                    Labels = ArrayNameCKN;
                    Formatter = Values => Values.ToString("N") + " VNĐ";
                }
            } 
        }
        public ICommand SelectionChangedCommand { get; set; } 
        public ThongKeViewModel()
        {
            //THỐNG KÊ THEO CHU KỲ
            ListChuKi = new ObservableCollection<ObjectChuKiDoanhThu>()
            {
                new ObjectChuKiDoanhThu(){NameChuKi = "Tháng"},
                new ObjectChuKiDoanhThu(){NameChuKi = "Năm"}
            };

            NgayHienTai = DateTime.Now;
            NgayDauThang = DateTime.Parse($"1/{NgayHienTai.Month}/{NgayHienTai.Year}");

            //THỐNG KÊ SỐ LƯỢNG SẢN PHẨN BÁN TRONG THÁNG
            SeriesCollection seriesSLSP = new SeriesCollection();

            var ThongKe = DataProvider.Ins.DB.TTHDBs.Where(x => x.HDB.NgayBan >= NgayDauThang && x.HDB.NgayBan <= NgayHienTai).GroupBy(t => t.SanPham.LoaiSanPham.TenLoaiSP).Select(x => new { TenLSP = x.Key, Sum = x.Sum(t => t.SL)}).ToList();

            foreach (var item in ThongKe)
            {
                seriesSLSP.Add(new PieSeries() { Title = item.TenLSP.ToString(), Values = new ChartValues<ObservableValue> { new ObservableValue((double)item.Sum) }, DataLabels = true });
            }
            PieChartSLSP = seriesSLSP;

            //THỐNG KÊ SẢN PHẨM BÁN CHẠY TRONG THÁNG
            SeriesCollection seriesTOP_SP = new SeriesCollection();

            var top3List = DataProvider.Ins.DB.TTHDBs.Where(x => x.HDB.NgayBan >= NgayDauThang && x.HDB.NgayBan <= NgayHienTai).GroupBy(t => t.SanPham).Select(x => new { SP = x.Key, Sum = x.Sum(t => t.SL) }).OrderByDescending(t=>t.Sum).Take(3);

            foreach (var item in top3List)
            {
                seriesTOP_SP.Add(new PieSeries() { Title = item.SP.TenSP, Values = new ChartValues<ObservableValue> {new ObservableValue((double)item.Sum) }, DataLabels = true });
            }
            PieChartTOP_SP = seriesTOP_SP;

            //XÉT CHỈ TIÊU KHÁCH HÀNG TIỀM NĂNG
            var KhachHangTOP = DataProvider.Ins.DB.HDBs.GroupBy(t => t.KhachHang).Select(x => new { KH = x.Key, Sum = x.Sum(t => t.TongTien)}).OrderByDescending(t => t.Sum).Take(1).ToList().SingleOrDefault();
            IDKhachHangTiemNang = KhachHangTOP.KH.Id.ToString();
            KhachHangTiemNang = KhachHangTOP.KH.TenKH.ToString();

            //DOANH THU SO VỚI THÁNG TRƯỚC
            //DOANH THU THÁNG HIỆN TẠI
            double DoanhThuThangHienTai = (double)DataProvider.Ins.DB.HDBs.Where(x => x.NgayBan >= NgayDauThang && x.NgayBan <= NgayHienTai).Sum(t=>t.TongTien);
            //DOANH THU THÁNG HIỆN TẠI
            DateTime DauThangTruoc = DateTime.Parse($"1/{NgayHienTai.Month - 1}/{NgayHienTai.Year}");
            string ngaythangtruoc = DateTime.DaysInMonth(NgayHienTai.Year, NgayHienTai.Month - 1).ToString();
            string thangtruoc = ($"{ngaythangtruoc}/{NgayHienTai.Month - 1}/{NgayHienTai.Year}");
            DateTime CuoiThangTruoc = DateTime.ParseExact(thangtruoc, "d/M/yyyy", CultureInfo.InvariantCulture);
            double DoanhThuThangTruoc = (double)DataProvider.Ins.DB.HDBs.Where(x => x.NgayBan >= DauThangTruoc && x.NgayBan <= CuoiThangTruoc).Sum(t => t.TongTien);

            //SO SÁNH
            if (DoanhThuThangTruoc == null)
            {
                DoanhThuVSThangTruoc = (double)(DoanhThuThangHienTai - 0);
                ChangesIcon = MaterialDesignThemes.Wpf.PackIconKind.Upload;
            }
            else
            {
                if (DoanhThuThangHienTai > DoanhThuThangTruoc)
                {
                    DoanhThuVSThangTruoc = (double)(DoanhThuThangHienTai - DoanhThuThangTruoc);
                    ChangesIcon = MaterialDesignThemes.Wpf.PackIconKind.Upload;
                }
                else
                {
                    DoanhThuVSThangTruoc = (double)(DoanhThuThangTruoc - DoanhThuThangHienTai);
                    ChangesIcon = MaterialDesignThemes.Wpf.PackIconKind.Download;
                }
            }

            //NHÂN VIÊN NĂNG XUẤT NHẤT
            var NhanVienTOP = DataProvider.Ins.DB.HDBs.GroupBy(t => t.NhanVien).Select(x => new { NV = x.Key, Sum = x.Sum(t => t.TongTien) }).OrderByDescending(t => t.Sum).Take(1).ToList().SingleOrDefault();
            IDNhanVienTiemNang = NhanVienTOP.NV.TenNV.ToString();
            NhanVienTiemNang = NhanVienTOP.NV.Id.ToString();

            
        }
    }
}
