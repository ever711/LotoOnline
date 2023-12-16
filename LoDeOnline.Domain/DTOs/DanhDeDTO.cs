using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class DanhDeDTO
    {
        public DanhDeDTO()
        {
            State = "draft";
            Date = DateTime.Today;
            Lines = new List<DanhDeLineDTO>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Trạng thái: draft, open
        /// </summary>
        public string State { get; set; }

        public string ShowState
        {
            get
            {
                switch(State)
                {
                    case "open":
                        return "Đã xác nhận";
                    case "done":
                        return "Đã dò kết quả";
                    case "cancel":
                        return "Hủy bỏ";
                    default:
                        return "Nháp";
                }
            }
            set { }
        }

        /// <summary>
        /// Ngày đánh
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Đài 
        /// </summary>
        public long DaiId { get; set; }
        public DaiXoSoDTO Dai { get; set; }
        public string DaiName { get; set; }

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
        public PartnerDTO Partner { get; set; }
        public string PartnerName { get; set; }

        public string Number { get; set; }

        public long CompanyId { get; set; }

        public IEnumerable<DanhDeLineDTO> Lines { get; set; }
    }
}
