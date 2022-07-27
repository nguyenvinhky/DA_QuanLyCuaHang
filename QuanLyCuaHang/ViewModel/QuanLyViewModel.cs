using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyCuaHang.ViewModel
{
    public class QuanLyViewModel : BaseViewModel
    {
        private int index;
        public ICommand TabNhanVien { get; set; }
        public ICommand TabNCC { get; set; }
        public ICommand TabLoaiSP { get; set; }
        public ICommand TabLoaiTK { get; set; }


        private object _tabQuanLyView;
        public object tabQuanLyView
        {
            get { return _tabQuanLyView; }
            set
            {
                _tabQuanLyView = value;
                OnPropertyChanged();
            }
        }

        public NhanVienViewModel NV { get; set; }
        public NCCViewModel NCC { get; set; }
        public LoaiSanPhamViewModel LSP { get; set; }
        public LoaiTKViewModel LTK { get; set; }


        public QuanLyViewModel()
        {
            NV = new NhanVienViewModel();
            NCC = new NCCViewModel();
            LSP = new LoaiSanPhamViewModel();
            LTK = new LoaiTKViewModel();

            tabQuanLyView = NV;

            TabNhanVien = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                tabQuanLyView = NV;
            });

            TabNCC = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                tabQuanLyView = NCC;

            });

            TabLoaiSP = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                tabQuanLyView = LSP;

            });

            TabLoaiTK = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                tabQuanLyView = LTK;

            });
        }
    }
}
