using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai2
{
    public class GiaoVien
    {
        public string MaSo {get;set;}
        public string HoTen{get;set;}
        public DateTime NgaySinh;
        public DanhMucMonHoc dsMonHoc;
public string GioiTinh;
public string[] NgoaiNgu;
public string SDT;
public string Mail; 
        public GiaoVien()
        {
            dsMonHoc = new DanhMucMonHoc();
            NgoaiNgu = new string[10];
        }
        public GiaoVien(string maSo, string hoTen, DateTime ngaySinh, DanhMucMonHoc ds, string gt, string []nn,string sdt, string mail)
        {
            this.MaSo = maSo;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.dsMonHoc = ds;
            this.GioiTinh = gt;
            this.NgoaiNgu = nn;
            this.Mail = mail;
            this.SDT = sdt;
        }
        public override string ToString()
        {
            string s = "Mã số:" + MaSo + "\n" + "Họ tên:" + HoTen + "\n" +
                 "Ngày sinh" + NgaySinh.ToString() + "\n" +
                "Giới tính:" + GioiTinh + "\n" +
                "SDT" + SDT + "\n" +
                "Mail" + Mail + "\n";
            string sNgoaiNgu = "Ngoại Ngữ: ";
            foreach (string t in NgoaiNgu)
                sNgoaiNgu += t + ";";
            string MonDay = " Danh sách môn dạy:";
            foreach (MonHoc mh in dsMonHoc.ds)
                MonDay += mh + ";";
            s += "\n" + sNgoaiNgu + "\n" + MonDay;

            return s;
        }
    }
}
