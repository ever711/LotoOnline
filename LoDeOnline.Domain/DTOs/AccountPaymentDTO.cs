using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class AccountPaymentDTO
    {
        public long Id { get; set; }

        public long? CompanyId { get; set; }

        public long? PartnerId { get; set; }
        public string PartnerName { get; set; }

        public string PartnerType { get; set; }

        public DateTime PaymentDate { get; set; }

        public long JournalId { get; set; }
        public string JournalName { get; set; }

        public string BankAccNumber { get; set; }

        public string State { get; set; }
        public string StateGet
        {
            get
            {
                switch (State)
                {
                    case "posted":
                        return "Đã vào sổ";
                    default:
                        return "Chưa vào sổ";
                }
            }
            set { }
        }

        public string Name { get; set; }

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// Mã giao dịch, chứng từ...
        /// </summary>
        public string Communication { get; set; }
    }
}
