using QuanLyCuaHang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang.ViewModel
{
    public class NCCViewModel : BaseViewModel
    {
        private ObservableCollection<NCC> _ListNCC;
        public ObservableCollection<NCC> ListNCC { get => _ListNCC; set => _ListNCC = value; }
        public NCCViewModel()
        {
            ListNCC = new ObservableCollection<NCC>(DataProvider.Ins.DB.NCCs.ToList());
        }

    }
}
