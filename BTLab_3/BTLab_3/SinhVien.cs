using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLab_3
{
   public class SinhVien
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Lop { get; set; }
        public string Hinh { get; set; }
        public bool GioiTinh { get; set; }
        public List<string> ChuyenNganh { get; set; }
        public SinhVien()
        {
            ChuyenNganh = new List<string>();
        }
        public SinhVien(string ms, string hoTen, DateTime ngaySinh, string diaChi, string lop, string hinh, bool gt, List<string> chuyenNganh)
        {
            this.MaSo = ms;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.DiaChi = diaChi;
            this.Lop = lop;
            this.Hinh = hinh;
            this.GioiTinh = gt;
            this.ChuyenNganh = chuyenNganh;

        }
    }
}
