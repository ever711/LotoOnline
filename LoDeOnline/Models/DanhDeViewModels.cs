using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Models
{
    public class CreateDanhDeViewModel
    {
        public DateTime Date { get; set; }

        public long DaiId { get; set; }

        public long LoaiDeId { get; set; }

        public string SoDanh { get; set; }

        /// <summary>
        /// Tiền một con
        /// </summary>
        public decimal Quantity { get; set; }
    }
}