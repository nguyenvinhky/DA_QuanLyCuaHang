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
    
    public partial class TaiKhoan
    {
        public int Id { get; set; }
        public string IdNV { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdLoaiTK { get; set; }
    
        public virtual LoaiTK LoaiTK { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
