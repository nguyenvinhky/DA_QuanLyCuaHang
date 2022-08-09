using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCuaHang.ViewModel
{
    public class SizeViewModel : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _LSP;
        public string LSP { get => _LSP; set { _LSP = value; OnPropertyChanged(); } }
        private string _KichThuoc;
        public string KichThuoc { get => _KichThuoc; set { _KichThuoc = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiSanPham> _ListCBB_LSP;
        public ObservableCollection<LoaiSanPham> ListCBB_LSP { get => _ListCBB_LSP; 
            set 
            { 
                _ListCBB_LSP = value;
                OnPropertyChanged(); 
            } 
        }
        private ObservableCollection<Model.Size> _ListSize;
        public ObservableCollection<Model.Size> ListSize { get => _ListSize; set { _ListSize = value; OnPropertyChanged(); } }
        private LoaiSanPham _SelectedId_LSP;
        public LoaiSanPham SelectedId_LSP 
        { 
            get => _SelectedId_LSP; 
            set 
            { 
                _SelectedId_LSP = value; 
                OnPropertyChanged();
                if (SelectedId_LSP == null)
                    return;
                LSP = SelectedId_LSP.TenLoaiSP;
            } 
        }

        private Model.Size _SelectedSize;
        public Model.Size SelectedSize 
        { 
            get => _SelectedSize; 
            set 
            { 
                _SelectedSize = value; 
                OnPropertyChanged();
                if (SelectedSize == null)
                    return;
                Id = SelectedSize.Id;
                LSP = SelectedSize.LoaiSanPham.TenLoaiSP;
                KichThuoc = SelectedSize.KichThuoc;
            } 
        }

        public ICommand AddSize { get; set; }
        public ICommand EditSize { get; set; }
        public ICommand DeleteSize { get; set; }
        public SizeViewModel()
        {
            //Load combobox loại sản phẩm
            ListCBB_LSP = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);

            //Load list Size
            ListSize = new ObservableCollection<Model.Size>(DataProvider.Ins.DB.Sizes);

            AddSize = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(KichThuoc))
                {
                    return false;
                }

                var s = DataProvider.Ins.DB.Sizes.Where(x => x.IdLSP == SelectedId_LSP.Id && x.KichThuoc == this.KichThuoc);
                if (s == null || s.Count() != 0)
                {
                    return false;
                }
                return true; 
            },(p) => 
            {
                var s = new Model.Size() { IdLSP = SelectedId_LSP.Id, KichThuoc = KichThuoc};

                DataProvider.Ins.DB.Sizes.Add(s);
                DataProvider.Ins.DB.SaveChanges();

                ListSize.Add(s);
            });

            EditSize = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(KichThuoc))
                {
                    return false;
                }

                var size = DataProvider.Ins.DB.Sizes.Where(x => x.IdLSP == SelectedId_LSP.Id && x.KichThuoc == this.KichThuoc);
            if (size == null || size.Count() != 0 || SelectedSize == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var GetIdSize = DataProvider.Ins.DB.Sizes.Where(x => x.Id == SelectedSize.Id).SingleOrDefault();
                GetIdSize.IdLSP = SelectedId_LSP.Id;
                GetIdSize.KichThuoc = KichThuoc;
                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
