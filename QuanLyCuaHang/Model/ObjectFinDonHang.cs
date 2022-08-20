using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang.Model
{
    public class ObjectFinDonHang
    {
        private int _IDNameFind;

        public int IDNameFind { get => _IDNameFind; set => _IDNameFind = value; }
        private string _NameFind;

        public string NameFind { get => _NameFind; set => _NameFind = value; }
    }
}
