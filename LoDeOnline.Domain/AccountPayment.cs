using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountPayment: Entity
    {
        public AccountPayment()
        {
            MoveLines = new List<AccountMoveLine>();
            State = "draft";
        }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long CurrencyId { get; set; }
        public virtual ResCurrency Currency { get; set; }

        public long? PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public string PartnerType { get; set; }

        public DateTime PaymentDate { get; set; }

        public long JournalId { get; set; }
        public virtual AccountJournal Journal { get; set; }

        public string State { get; set; }

        public string Name { get; set; }

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// Mã giao dịch, chứng từ...
        /// </summary>
        public string Communication { get; set; }

        /// <summary>
        /// Tên người gửi
        /// </summary>
        public string Sender { get; set; }

        public virtual IList<AccountMoveLine> MoveLines { get; set; }
    }
}
