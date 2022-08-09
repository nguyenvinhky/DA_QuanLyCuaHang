using QuanLyCuaHang.Form;
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
    public class KhachHangViewModel:BaseViewModel
    {
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _TenKH;
        public string TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private ObservableCollection<KhachHang> _ListKhachHang;
        public ObservableCollection<KhachHang> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }
        private ObservableCollection<ObjectGioiTinh> _ListGT;
        public ObservableCollection<ObjectGioiTinh> ListGT { get => _ListGT; set { _ListGT = value; OnPropertyChanged(); } }

        private ObjectGioiTinh _SelectedGT;
        public ObjectGioiTinh SelectedGT 
        { 
            get => _SelectedGT; 
            set 
            { 
                _SelectedGT = value; 
                OnPropertyChanged();
                if (SelectedGT == null)
                    return;
            } 
        }

        private KhachHang _SelectedItemKH;
        public KhachHang SelectedItemKH
        {
            get => _SelectedItemKH;
            set
            {
                _SelectedItemKH = value;
                OnPropertyChanged();
                if (SelectedItemKH == null)
                    return;
                FormTTCTKH ctkh = new FormTTCTKH();
                Id = SelectedItemKH.Id;
                TenKH = SelectedItemKH.TenKH;
                GioiTinh = SelectedItemKH.GioiTinh;
                SDT = SelectedItemKH.SDT;
                DiaChi = SelectedItemKH.DiaChi;
                ctkh.ShowDialog();
            }
        }
        public ICommand OpenFormAddKH { get; set; }
        public ICommand AddKH { get; set; }
        public ICommand EditKH { get; set; }
        public KhachHangViewModel()
        {
            ListGT = new ObservableCollection<ObjectGioiTinh>()
            {
               new ObjectGioiTinh(){GioiTinh = "Nam"},
               new ObjectGioiTinh(){GioiTinh = "Nữ"},
            };
            
            ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);

            OpenFormAddKH = new RelayCommand<Window>((p) => 
            { 
                return true;
            },(p) => 
            {
                FormThemKhachHang fAddKH = new FormThemKhachHang();
                Id = createIdKH("KHJDI_");
                fAddKH.ShowDialog();
            });

            AddKH = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var kh = new KhachHang() { Id = Id, TenKH = TenKH, GioiTinh = SelectedGT.GioiTinh, SDT = SDT, DiaChi = DiaChi };
                DataProvider.Ins.DB.KhachHangs.Add(kh);
                DataProvider.Ins.DB.SaveChanges();
                ListKhachHang.Add(kh);

                p.Close();
            });

            EditKH = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var editkh = DataProvider.Ins.DB.KhachHangs.Where(x => x.Id == SelectedItemKH.Id).SingleOrDefault();
                editkh.TenKH = TenKH;
                editkh.GioiTinh = GioiTinh;
                editkh.SDT = SDT;
                editkh.DiaChi = DiaChi;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã cập nhật thành công !", "Thông báo");
                p.Close();
            });
        }

        string createIdKH(string valueID)
        {
            int STT = 0;
            string tmp = "";
            valueID += "001";
            if (ListKhachHang.Count() == 0 || ListKhachHang == null)
            {
                return valueID;
            }
            else
            {
                foreach (var item in ListKhachHang)
                {
                    while (item.Id == valueID)
                    {
                        tmp = "KHJDI_";
                        STT = STT + 1;
                        string id = "000" + STT;
                        id = id.Substring(id.Length - 3, 3);
                        valueID = tmp + id;
                    }
                }
            }
            return valueID;
        }
    }
}
