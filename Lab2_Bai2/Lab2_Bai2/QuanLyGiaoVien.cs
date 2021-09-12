using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai2
{
   public class QuanLyGiaoVien
    {
		public List<GiaoVien> dsGiaoVien;
		public QuanLyGiaoVien()
		{
			dsGiaoVien = new List<GiaoVien>();
		}

		public GiaoVien this[int index]
		{
            get { return dsGiaoVien[index]; }
			set { this.dsGiaoVien[index] = value; }
		}

		public bool Them(GiaoVien gv)
		{
			var add = dsGiaoVien.Exists(x => x.MaSo == gv.MaSo);
            if (add) return false;
			dsGiaoVien.Add(gv);
			return true;
		}

		public GiaoVien Find(string value, KieuTim kieuTim)
		{
			GiaoVien gv = null;

			switch (kieuTim)
			{
				case KieuTim.TheoHoTen:
					gv = dsGiaoVien.Find(x => x.HoTen == value.ToString());
					break;
				case KieuTim.TheoMa:
					gv = dsGiaoVien.Find(x => x.MaSo == value.ToString());
					gv.ToString();
					break;
				case KieuTim.TheoSDT:
					gv = dsGiaoVien.Find(x => x.SDT == value.ToString());
					break;
			}
			return gv;
		}
	}
}
