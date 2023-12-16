using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class LoaiDe: Entity
    {
        public LoaiDe()
        {
            Rules = new List<LoaiDeRule>();
            Type = "normal";
        }

        /// <summary>
        /// Reference, Description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thanh toan cho 1K/con
        /// </summary>
        public decimal? ThanhToan1K { get; set; }

        /// <summary>
        /// Thắng gấp
        /// </summary>
        public decimal? ThangGap { get; set; }

        /// <summary>
        /// Số thấp nhất đc quyền đánh dùng để render ra bảng chọn số
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// Số cao nhất đc quyền đánh
        /// </summary>
        public int? MaxValue { get; set; }

        public virtual IList<LoaiDeRule> Rules { get; set; }

        public long? LoDeCategId { get; set; }
        public virtual LoDeCategory LoDeCateg { get; set; }

        public long? LoaiDeCategId { get; set; }
        public virtual LoaiDeCategory LoaiDeCateg { get; set; }

        /// <summary>
        /// Tìm n lần
        /// </summary>
        public bool? Multi { get; set; }

        /// <summary>
        /// Bình thường, xiên, trượt xiên
        /// </summary>
        public string Type { get; set; }

        public int? SoLuongXien { get; set; }
    }
}
