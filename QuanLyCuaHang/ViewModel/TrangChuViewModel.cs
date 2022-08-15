using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHang.ViewModel;
using System.Collections.ObjectModel;
using QuanLyCuaHang.Model;
using System.Windows.Input;
using System.Windows;

namespace QuanLyCuaHang.ViewModel
{
    public class TrangChuViewModel:BaseViewModel
    {
        private DateTime Now = DateTime.Today;
        public int _SDB;
        public int SDB { get => _SDB; set { _SDB = value; OnPropertyChanged(); } }
        private ObservableCollection<HDB> _ListHDB;
        public ObservableCollection<HDB> ListHDB { get => _ListHDB; set { _ListHDB = value; OnPropertyChanged(); } }
        public double _DoanhThuNgay;
        public double DoanhThuNgay { get => _DoanhThuNgay; set { _DoanhThuNgay = value; OnPropertyChanged(); } }
        public int _SLSP_BanTrongNgay;
        public int SLSP_BanTrongNgay { get => _SLSP_BanTrongNgay; set { _SLSP_BanTrongNgay = value; OnPropertyChanged(); } }
        private SeriesCollection _Chart;
        public SeriesCollection Chart { get => _Chart; set { _Chart = value; OnPropertyChanged(); } }
        public string[] _Labels;
        public string[] Labels { get => _Labels; set { _Labels = value; OnPropertyChanged(); } }
        private ObservableCollection<TTHDB> _ListTTHDB;
        public ObservableCollection<TTHDB> ListTTHDB { get => _ListTTHDB; set { _ListTTHDB = value; OnPropertyChanged(); } }
        public TrangChuViewModel()
        {
            SDB = 0;
            DoanhThuNgay = 0;
            SLSP_BanTrongNgay = 0;


            SDB = new ObservableCollection<HDB>(DataProvider.Ins.DB.HDBs.Where(x => x.NgayBan.Value.Day == Now.Day && x.NgayBan.Value.Month == Now.Month && x.NgayBan.Value.Year == Now.Year)).Count();

            ListHDB = new ObservableCollection<HDB>(DataProvider.Ins.DB.HDBs.Where(x => x.NgayBan.Value.Day == Now.Day && x.NgayBan.Value.Month == Now.Month && x.NgayBan.Value.Year == Now.Year));

            //Doanh thu trong ngày
            foreach (HDB item in ListHDB)
            {
                DoanhThuNgay += (double)item.TongTien;
            }

            ListTTHDB = new ObservableCollection<TTHDB>(DataProvider.Ins.DB.TTHDBs.Where(x => x.HDB.NgayBan.Value.Day == Now.Day && x.HDB.NgayBan.Value.Month == Now.Month && x.HDB.NgayBan.Value.Year == Now.Year));
            //Số lượng sản phẩm bán ra trong ngày
            foreach (TTHDB item in ListTTHDB)
            {
                SLSP_BanTrongNgay += (int)item.SL;
            }

            //Số lượng sản phẩm bán ra theo giờ
            List<int> ArraySLSP = new List<int>();
            SeriesCollection series = new SeriesCollection();
            int sl = 0;
            for (int i = 0; i < 24; i++)
            {
                foreach (TTHDB item in ListTTHDB)
                {
                    if (item.HDB.NgayBan.Value.Hour == i)
                    {
                        sl += (int)item.SL;
                    }
                }
                ArraySLSP.Add(sl);
                sl = 0;
            }
            List<int> ArraySLKH = new List<int>();
            int slkh = 0;
            for (int i = 0; i < 24; i++)
            {
                foreach (HDB item in ListHDB)
                {
                    if (item.NgayBan.Value.Hour == i)
                    {
                        slkh += 1;
                    }
                }
                ArraySLKH.Add(slkh);
                slkh = 0;
            }

            series.Add(new ColumnSeries() { Title = "Sản phẩm", Values = new ChartValues<int>(ArraySLSP) });
            series.Add(new ColumnSeries() { Title = "Khách hàng", Values = new ChartValues<int>(ArraySLKH) });
            Chart = series;

            Labels = new[] { "0h AM", "1h AM", "2h AM", "3h AM", "4h AM", "5h AM", "6h AM", "7h AM", "8h AM", "9h AM", "10h AM", "11h AM", "12h PM", "1h PM", "2h PM", "3h PM", "4h PM", "5h PM", "6h PM", "7h PM", "8h PM", "9h PM", "10h PM", "11 PM", "12h PM" };
        }
    }
}
