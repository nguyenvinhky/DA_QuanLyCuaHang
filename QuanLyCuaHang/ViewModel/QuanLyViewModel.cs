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
        private int location;
        public ICommand TabButtonCommand { get; set; }
        public ICommand Gridcursor { get; set; }


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


        public QuanLyViewModel()
        {
            NV = new NhanVienViewModel();
            NCC = new NCCViewModel();

            TabButtonCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                index = int.Parse(p.Uid);

                switch (index)
                {
                    case 0:
                        {
                            tabQuanLyView = NV;
                            p.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        }

                    case 1:
                        {
                            tabQuanLyView = NCC;
                            p.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        }

                    case 2:
                        {
                            tabQuanLyView = NV;
                            p.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        }

                    case 3:
                        {
                            tabQuanLyView = NV;
                            p.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        }

                    case 4:
                        {
                            tabQuanLyView = NV;
                            p.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        }
                }
                
            }
            );
        }
    }
}
