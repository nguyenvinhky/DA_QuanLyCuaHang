//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCuaHang.Model
{
    using System;
    using System.Collections.Generic;
    using QuanLyCuaHang.ViewModel;

    public partial class TTHDB:BaseViewModel
    {
        private string id;
        private string idSP;
        private int idSize;
        private string idHDB;
        private Nullable<int> sL;

        public virtual HDB HDB { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual Size Size { get; set; }
        public string Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string IdSP { get => idSP; set { idSP = value; OnPropertyChanged(); } }
        public int IdSize { get => idSize; set { idSize = value; OnPropertyChanged(); } }
        public string IdHDB { get => idHDB; set { idHDB = value; OnPropertyChanged(); } }
        public int? SL { get => sL; set { sL = value; OnPropertyChanged(); } }
    }
}
