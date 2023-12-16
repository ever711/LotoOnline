using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoDeOnline.Domain;
using System.Web.Mvc;
namespace LoDeOnline.Models
{
   /* public class TinTucBoard
    {
        public IList<TinTuc> TinTucs { get; set; }
    }
    */
    public class TinTuc
    {
       

        public int MaTin { get; set; }
        public string TieuDe { get; set; }
        [AllowHtml]
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}