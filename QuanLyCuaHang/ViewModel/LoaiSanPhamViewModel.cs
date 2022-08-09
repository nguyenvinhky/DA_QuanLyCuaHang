using QuanLyCuaHang.Model;
using QuanLyCuaHang.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    public class LoaiSanPhamViewModel : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _TenLSP;
        public string TenLSP { get => _TenLSP; set { _TenLSP = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiSanPham> _ListLSP;
        public ObservableCollection<LoaiSanPham> ListLSP { get => _ListLSP; set { _ListLSP = value; OnPropertyChanged(); } }
        private LoaiSanPham _SelectedLSP;
        public LoaiSanPham SelectedLSP
        {
            get => _SelectedLSP;
            set
            {
                _SelectedLSP = value;
                OnPropertyChanged();
                if (SelectedLSP == null)
                    return;
                Id = SelectedLSP.Id;
                TenLSP = SelectedLSP.TenLoaiSP;
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public LoaiSanPhamViewModel()
        {
            ListLSP = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLSP))
                {
                    return false;
                }

                var listTen = DataProvider.Ins.DB.LoaiSanPhams.Where(n => n.TenLoaiSP == TenLSP);
                if (listTen == null || listTen.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var LoaiSanPham = new LoaiSanPham() { TenLoaiSP = this.TenLSP};

                DataProvider.Ins.DB.LoaiSanPhams.Add(LoaiSanPham);
                DataProvider.Ins.DB.SaveChanges();

                ListLSP.Add(LoaiSanPham);

                ViewSize vs = new ViewSize();
                var x = vs.DataContext as SizeViewModel;
                x.ListCBB_LSP.Add(LoaiSanPham);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLSP))
                {
                    return false;
                }
                var listTen = DataProvider.Ins.DB.LoaiSanPhams.Where(n => n.TenLoaiSP == TenLSP);
                if (listTen == null || listTen.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var GetIdLSP = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.Id == SelectedLSP.Id).SingleOrDefault();
                GetIdLSP.TenLoaiSP = TenLSP;
                DataProvider.Ins.DB.SaveChanges();
                ListLSP = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            });
        }
    }
}
