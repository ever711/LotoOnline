using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class DanhDeLineDTO
    {
        public long Id { get; set; }

        public string SoDanh { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal Quantity { get; set; }

        public decimal? PriceSubtotal { get; set; }

        /// <summary>
        /// Loại đề
        /// </summary>
        public long LoaiDeId { get; set; }
        public LoaiDeDTO LoaiDe { get; set; }
        
        public bool? IsXien { get; set; }

        public IEnumerable<DanhDeLineXienDTO> XienNumbers { get; set; }
    }
}
