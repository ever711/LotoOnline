using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Models
{
    public class LoaiDeBoard
    {
        public IList<LoaiDeSimple> LoaiDes { get; set; }
    }

    public class LoaiDeSimple
    {
        public long Id { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Cho phép đánh nhiều số 1 lần hay không? xiên thì ko dc
        /// </summary>
        public int Multi { get; set; }

        /// <summary>
        /// Thanh toan cho 1K/con
        /// </summary>
        public decimal? ThanhToan1K { get; set; }

        /// <summary>
        /// Thắng gấp
        /// </summary>
        public decimal? ThangGap { get; set; }

        public int? SoLuongXien { get; set; }

        public int? Min { get; set; }

        public int? Max { get; set; }
    }
}