using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Models
{
    public class DoSoViewModel
    {
        public DateTime Date { get; set; }

        public DateTime PreviousDate { get; set; }

        public IEnumerable<DaiXoSoViewModel> Dais { get; set; }
    }

    public class HomeIndexViewModel
    {
        /// <summary>
        /// Ngày cuối cùng có kết quả xổ số
        /// </summary>
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> Dais { get; set; }
    }
}