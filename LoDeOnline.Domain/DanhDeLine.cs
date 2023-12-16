using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class DanhDeLine: Entity
    {
        public DanhDeLine()
        {
            XienNumbers = new List<DanhDeLineXien>();
        }

        public long DanhDeId { get; set; }
        public virtual DanhDe DanhDe { get; set; }

        public string SoDanh { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal Quantity { get; set; }

        public decimal? PriceSubtotal { get; set; }

        /// <summary>
        /// Loại đề
        /// </summary>
        public long LoaiDeId { get; set; }
        public virtual LoaiDe LoaiDe { get; set; }

        public long? DaiId { get; set; }
        public virtual DaiXoSo Dai { get; set; }

        public virtual IList<DanhDeLineXien> XienNumbers { get; set; }
    }
}
