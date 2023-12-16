using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class LoaiDeDTO
    {
        public LoaiDeDTO()
        {
            Rules = new List<LoaiDeRuleDTO>();
            Active = true;
            ThanhToan1K = 0;
            ThangGap = 0;
            MinValue = 0;
            MaxValue = 0;
            Multi = true;
            Type = "normal";
        }

        public long Id { get; set; }

        /// <summary>
        /// Đánh lô, đầu đuôi
        /// </summary>
        public string Name { get; set; }

        public bool? Active { get; set; }

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

        public virtual IList<LoaiDeRuleDTO> Rules { get; set; }

        public long? LoDeCategId { get; set; }
        public LoDeCategoryDTO LoDeCateg { get; set; }
        public string LoDeCategName { get; set; }

        public long? LoaiDeCategId { get; set; }
        public LoaiDeCategoryDTO LoaiDeCateg { get; set; }
        public string LoaiDeCategName { get; set; }

        /// <summary>
        /// Tìm n lần
        /// </summary>
        public bool? Multi { get; set; }

        public string Type { get; set; }

        public int? SoLuongXien { get; set; }
    }
}
