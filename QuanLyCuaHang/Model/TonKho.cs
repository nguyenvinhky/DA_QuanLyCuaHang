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

    public partial class TonKho:BaseViewModel
    {
        private string idSP;
        private int idSize;
        private Nullable<int> sLTon;

        public virtual SanPham SanPham { get; set; }
        public virtual Size Size { get; set; }
        public string IdSP { get => idSP; set { idSP = value; OnPropertyChanged(); } }
        public int IdSize { get => idSize; set { idSize = value; OnPropertyChanged(); } }
        public int? SLTon { get => sLTon; set { sLTon = value; OnPropertyChanged(); } }
    }
}
