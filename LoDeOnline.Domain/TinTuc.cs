using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoDeOnline.Domain
{
  public  class TinTuc
    {
        private int maTin;
        private string tieuDe;
        private string noiDung;
        private DateTime thoiGian;

        public int MaTin { get => maTin; set => maTin = value; }
        public string TieuDe { get => tieuDe; set => tieuDe = value; }
       
        public string NoiDung { get => noiDung; set => noiDung = value; }
        public DateTime ThoiGian { get => thoiGian; set => thoiGian = value; }
    }
}
