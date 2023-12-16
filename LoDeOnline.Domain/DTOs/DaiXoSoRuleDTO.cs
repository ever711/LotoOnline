using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class DaiXoSoRuleDTO
    {
        public long Id { get; set; }

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
