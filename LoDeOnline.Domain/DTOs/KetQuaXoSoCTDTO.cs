using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class KetQuaXoSoCTDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// Giải tám, bảy....
        /// </summary>
        public string Giai { get; set; }

        /// <summary>
        /// Số trúng
        /// </summary>
        public string SoTrung { get; set; }
    }
}
