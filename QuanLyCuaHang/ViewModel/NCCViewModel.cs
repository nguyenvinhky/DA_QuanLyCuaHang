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
    public class NCCViewModel : BaseViewModel
    {

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private ObservableCollection<NCC> _ListNCC;
        public ObservableCollection<NCC> ListNCC { get => _ListNCC; set { _ListNCC = value; OnPropertyChanged(); } }
        private NCC _SelectedNCC;
        public NCC SelectedNCC 
        {
            get => _SelectedNCC;
            set 
            { 
                _SelectedNCC = value;
                OnPropertyChanged();
                if (SelectedNCC==null)
                    return;
                Ten = SelectedNCC.TenNCC;
                DiaChi = SelectedNCC.DiaChi;
                SDT = SelectedNCC.SDT;
                Email = SelectedNCC.Email;
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public NCCViewModel()
        {
            ListNCC = new ObservableCollection<NCC>(DataProvider.Ins.DB.NCCs);

            AddCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(Email))
                {
                    return false;
                }
                var listTen = DataProvider.Ins.DB.NCCs.Where(n => n.TenNCC == Ten);
                if (listTen == null || listTen.Count()!=0)
                {
                    return false;
                }
                return true;
            }, (p) => 
            {
                var NhaCungCap = new NCC() { TenNCC = this.Ten, DiaChi = this.DiaChi, SDT = this.SDT, Email = this.Email };

                DataProvider.Ins.DB.NCCs.Add(NhaCungCap);
                DataProvider.Ins.DB.SaveChanges();

                ListNCC.Add(NhaCungCap);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(Email) || SelectedNCC==null)
                {
                    return false;
                }
                var listTen = DataProvider.Ins.DB.NCCs.Where(x => x.TenNCC == Ten && x.DiaChi == DiaChi && x.SDT == SDT && x.Email == Email);
                if (listTen == null || listTen.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var GetIdNCC = DataProvider.Ins.DB.NCCs.Where(x => x.Id == SelectedNCC.Id).SingleOrDefault();
                GetIdNCC.TenNCC = Ten;
                GetIdNCC.DiaChi = DiaChi;
                GetIdNCC.SDT = SDT;
                GetIdNCC.Email = Email;
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
