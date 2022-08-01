using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{

    public class LoaiTKViewModel : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _TenLTK;
        public string TenLTK { get => _TenLTK; set { _TenLTK = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiTK> _ListLTK;
        public ObservableCollection<LoaiTK> ListLTK { get => _ListLTK; set { _ListLTK = value; OnPropertyChanged(); } }
        private LoaiTK _SelectedLTK;
        public LoaiTK SelectedLTK
        {
            get => _SelectedLTK;
            set
            {
                _SelectedLTK = value;
                OnPropertyChanged();
                if (SelectedLTK == null)
                    return;
                Id = SelectedLTK.Id;
                TenLTK = SelectedLTK.TenLoaiTK;
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public LoaiTKViewModel()
        {
            ListLTK = new ObservableCollection<LoaiTK>(DataProvider.Ins.DB.LoaiTKs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLTK))
                {
                    return false;
                }

                var listTen = DataProvider.Ins.DB.LoaiTKs.Where(n => n.TenLoaiTK == TenLTK);
                if (listTen == null || listTen.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var loaiTK = new LoaiTK() { TenLoaiTK = this.TenLTK };

                DataProvider.Ins.DB.LoaiTKs.Add(loaiTK);
                DataProvider.Ins.DB.SaveChanges();

                ListLTK.Add(loaiTK);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLTK))
                {
                    return false;
                }
                var listTen = DataProvider.Ins.DB.LoaiTKs.Where(n => n.TenLoaiTK == TenLTK);
                if (listTen == null || listTen.Count() != 0 || SelectedLTK == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var GetIdLSP = DataProvider.Ins.DB.LoaiTKs.Where(x => x.Id == SelectedLTK.Id).SingleOrDefault();
                GetIdLSP.TenLoaiTK = TenLTK;
                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
