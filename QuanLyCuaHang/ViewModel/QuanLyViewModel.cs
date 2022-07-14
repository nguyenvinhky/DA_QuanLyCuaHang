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
        public ICommand TabButtonCommand { get; set; }


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

            TabButtonCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                index = int.Parse(p.Uid);

                switch (index)
                {
                    case 0:
                        {
                            tabQuanLyView = NV;
                            break;
                        }

                    case 1:
                        {
                            tabQuanLyView = NCC;
                            break;
                        }

                    case 2:
                        {
                            tabQuanLyView = LSP;
                            break;
                        }

                    case 3:
                        {
                            tabQuanLyView = LTK;
                            break;
                        }

                    default :
                        {
                            break;
                        }
                }
                
            }
            );
        }
    }
}
