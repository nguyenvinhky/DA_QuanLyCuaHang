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
    
    public partial class TTHDB
    {
        public string Id { get; set; }
        public string IdSP { get; set; }
        public string IdHDB { get; set; }
        public Nullable<int> SL { get; set; }
        public string GhiChu { get; set; }
    
        public virtual HDB HDB { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
