//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLConTy_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHANSU
    {
        public string MaNV { get; set; }
        public string HovaTendem { get; set; }
        public string Ten { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string CCCD { get; set; }
        public string MaPB { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string MaCV { get; set; }
        public string TrinhDo { get; set; }
        public NHANSU(string MaNV, string HoDem, string Ten, DateTime NgaySinh, string DiaChi, string CCCD, string MaPB, string MaCV, string GioiTinh, string SDT, string Email)
        {
            this.MaNV = MaNV;
            this.HovaTendem = HoDem;
            this.Ten = Ten;
            this.NgaySinh = NgaySinh;
            this.DiaChi = DiaChi;
            this.CCCD = CCCD;
            this.MaPB = MaPB;
            this.MaCV = MaCV;
            this.GioiTinh = GioiTinh;
            this.SDT = SDT;
            this.Email = Email;
        }
    }
}