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
    
    public partial class TTHDN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTHDN()
        {
            this.TTHDBs = new HashSet<TTHDB>();
        }
    
        public string Id { get; set; }
        public string IdSP { get; set; }
        public string IdHDN { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<double> GiaNhap { get; set; }
        public Nullable<double> GiaBan { get; set; }
        public string GhiChu { get; set; }
    
        public virtual HDN HDN { get; set; }
        public virtual SanPham SanPham { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TTHDB> TTHDBs { get; set; }
    }
}
