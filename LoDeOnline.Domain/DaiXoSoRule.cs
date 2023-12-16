using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Lưu lại thông tin như đài gia lại xổ vào thứ 6
    /// </summary>
    public class DaiXoSoRule: Entity
    {
        public long DaiId { get; set; }
        public virtual DaiXoSo Dai { get; set; }

        /// <summary>
        /// Thứ
        /// </summary>
        public string Thu { get; set; }

        /// <summary>
        /// Giờ mở số
        /// </summary>
        public DateTime? TimeOpen { get; set; }
    }
}
