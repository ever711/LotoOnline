using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class LoaiDeRuleDTO
    {
        public LoaiDeRuleDTO()
        {
            GiaiDanh = "all";
            ViTriDanh = "chu_so_cuoi";
            KieuDanh = "danh_lo";
            Cumulative = false;
            SoLuongDanh = 1;
            SoLuongXien = 0;
        }

        public long Id { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tất cả các giải hoặc chọn giải sẽ đánh
        /// </summary>
        public string GiaiDanh { get; set; }

        /// <summary>
        /// Vị trí đánh: đánh số cuối, đánh hàng chục
        /// </summary>
        public string ViTriDanh { get; set; }

        /// <summary>
        /// Số lượng đánh
        /// </summary>
        public int SoLuongDanh { get; set; }

        /// <summary>
        /// kiểu đánh: đánh lô, đánh xiên, đánh trượt xiên
        /// </summary>
        public string KieuDanh { get; set; }

        /// <summary>
        /// Số lượng xiên
        /// </summary>
        public int? SoLuongXien { get; set; }

        /// <summary>
        /// Tích lũy
        /// </summary>
        public bool? Cumulative { get; set; }
    }
}
