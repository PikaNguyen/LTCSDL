using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_WindApplication
{
    public class QuanLySinhVien
    {
        public delegate int SoSanh(object sv1, object sv2);

        public List<SinhVien> DanhSach;
        public QuanLySinhVien()
        {
            DanhSach = new List<SinhVien>();

        }
        public void Add(SinhVien sv)
        {
            this.DanhSach.Add(sv);
        }
        public SinhVien this[int index]
        {
            get { return DanhSach[index]; }
            set { DanhSach[index] = value; }
        }
        public void DocTuFile()
        {
            string fileN = "DSNV.txt", t;
            string[] s;
            SinhVien v;
            StreamReader sr = new StreamReader(new FileStream(fileN, FileMode.Open));
            while ((t = sr.ReadLine()) != null)
            {
                s = t.Split('\t');
                v = new SinhVien();
                v.MaSo = s[0];
                v.HoTen = s[1];
                v.Phai = false;
                if (s[2] == "1")
                    v.Phai = true;
                v.NgaySinh = DateTime.Parse(s[3]);
                v.Lop = s[4];
                v.SDT = s[5];
                v.Email = s[6];
                v.DiaChi = s[7];
                v.Hinh = s[8];
                this.Add(v);
            }
            sr.Close();
        }

        public bool Sua(SinhVien alpha, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;
            count = this.DanhSach.Count - 1;
            for (i = 0; i <= count; i++)
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = alpha;
                    kq = true;
                    break;
                }
            return kq;

        }

        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien sv = null;
            foreach (SinhVien sv1 in DanhSach)
            {
                if (ss(obj, sv1) == 0)
                {
                    sv = sv1;
                    break;
                }
            }
            return sv;

        }
        public void Xoa(object obj, SoSanh ss)
        {
            int i = DanhSach.Count - 1;
            for (; i >= 0; i--)
            {
                if (ss(obj, this[i]) == 0)
                    this.DanhSach.RemoveAt(i);
            }
        }
        public void GhiVaoFile(string s)
        {
            FileStream fs;
            FileInfo fi = new FileInfo(s);
            if (fi.Exists)
            {
                using (fs = new FileStream(s, FileMode.Truncate))
                {
                    using (var writer = new StreamWriter(fs))
                        foreach (var sv in DanhSach)
                        {
                           
                            writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", sv.MaSo, sv.HoTen, sv.Phai ? "1" : "0",
                                sv.NgaySinh.ToString(), sv.Lop, sv.SDT, sv.Email, sv.DiaChi, sv.Hinh);
                            writer.Flush();
                        }
                    fs.Close();
                }
            }
            else
            {
                fs = new FileStream(s, FileMode.OpenOrCreate);

            }
        }
        
    }
}
