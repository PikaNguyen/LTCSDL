using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai2
{
    public class MonHoc
    {
        public int Id { get; set; }
        public string TenMH { get; set; }
        public int SOTC { get; set;}
        public MonHoc() { 
        }
        public MonHoc(string ten)
        { this.TenMH = ten; }
        public MonHoc(int id, string ten, int tc) 
        {
            this.Id = id; this.TenMH = ten; this.SOTC = tc; 
        }
        public override string ToString()
        {
            return TenMH;
        }
    }
}
