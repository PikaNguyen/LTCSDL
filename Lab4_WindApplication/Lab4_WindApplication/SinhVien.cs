using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_WindApplication
{
    public class SinhVien
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool Phai { get; set; }
        public string Lop { get; set; }
        public string SDT { get; set; }
        public string Hinh { get; set; }
        public SinhVien()
        {

        }
        public SinhVien(string maSo, string hoTen, string mail, string diaChi, DateTime ngaySinh, bool phai, string lop, string sdt, string hinh)
        {
            this.MaSo = maSo;
            this.HoTen = hoTen;
            this.Email = mail;
            this.DiaChi = diaChi;
            this.NgaySinh = ngaySinh;
            this.Phai = phai;
            this.Lop = lop;
            this.SDT = sdt;
            this.Hinh = hinh;
        }
    }
    
}
