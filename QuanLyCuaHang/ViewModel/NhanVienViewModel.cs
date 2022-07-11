using QuanLyCuaHang.Form;
using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private NhanVien _SelectedNhanVien;
        public NhanVien SelectedNhanVien 
        { 
            get => _SelectedNhanVien;
            set 
            { 
                _SelectedNhanVien = value;
                if (SelectedNhanVien == null)
                    return;
                FormThongTinNhanVien ttnv = new FormThongTinNhanVien();
                ttnv.ShowDialog();
            }
        }
        private ObservableCollection<NhanVien> _NhanVienList;
        public ObservableCollection<NhanVien> NhanVienList { get => _NhanVienList; set { _NhanVienList = value; OnPropertyChanged(); } }


        public NhanVienViewModel()
        {
            NhanVienList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens.ToList());
        }
    }
}
