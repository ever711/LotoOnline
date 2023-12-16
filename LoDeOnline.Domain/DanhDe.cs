using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Đánh đề
    /// </summary>
    public class DanhDe : Entity
    {
        public DanhDe()
        {
            State = "draft";
            Lines = new List<DanhDeLine>();
        }

        public string Name { get; set; }

        /// <summary>
        /// Trạng thái: draft, open
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Ngày đánh
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Đài 
        /// </summary>
        public long DaiId { get; set; }
        public virtual DaiXoSo Dai { get; set; }

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal? AmountTotal { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        public string KetQua { get; set; }

        public long PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? MoveId { get; set; }
        public virtual AccountMove Move { get; set; }

        /// <summary>
        /// Bút toán trúng thưởng
        /// </summary>
        public long? TrungMoveId { get; set; }
        public virtual AccountMove TrungMove { get; set; }

        public string Number { get; set; }

        public string MoveName { get; set; }

        public virtual IList<DanhDeLine> Lines { get; set; }
    }
}
