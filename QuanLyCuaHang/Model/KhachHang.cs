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
    
    public partial class KhachHang:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            this.HDBs = new HashSet<HDB>();
        }

        private string id;
        private string tenKH;
        private string gioiTinh;
        private string sDT;
        private string diaChi;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HDB> HDBs { get; set; }
        public string Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string TenKH { get => tenKH; set { tenKH = value; OnPropertyChanged(); } }
        public string GioiTinh { get => gioiTinh; set { gioiTinh = value; OnPropertyChanged(); } }
        public string SDT { get => sDT; set { sDT = value; OnPropertyChanged(); } }
        public string DiaChi { get => diaChi; set { diaChi = value; OnPropertyChanged(); } }
    }
}
