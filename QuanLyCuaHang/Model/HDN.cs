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
    
    public partial class HDN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDN()
        {
            this.TTHDNs = new HashSet<TTHDN>();
        }
    
        public string Id { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<double> TongTienHDN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TTHDN> TTHDNs { get; set; }
    }
}
