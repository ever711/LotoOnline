using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class KetQuaXoSoCT: Entity
    {
        /// <summary>
        /// Giải tám, bảy....
        /// </summary>
        public string Giai { get; set; }

        /// <summary>
        /// Số trúng
        /// </summary>
        public string SoTrung { get; set; }

        public long KQXSId { get; set; }
        public virtual KetQuaXoSo KQXS { get; set; }
    }
}
