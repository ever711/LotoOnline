using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountJournal: Entity
    {
        public AccountJournal()
        {
            JournalUser = false;
            UpdatePosted = false;
            DedicatedRefund = false;
        }

        public string Code { get; set; }

        public long SequenceId { get; set; }
        public virtual IRSequence Sequence { get; set; }

        public string Name { get; set; }


        public string Type { get; set; }

        /// <summary>
        /// Allow Cancelling Entries
        /// </summary>
        public bool? UpdatePosted { get; set; }

        public long? CurrencyId { get; set; }
        public virtual ResCurrency Currency { get; set; }

        public long? DefaultDebitAccountId { get; set; }
        public virtual Account DefaultDebitAccount { get; set; }

        public long? DefaultCreditAccountId { get; set; }
        public virtual Account DefaultCreditAccount { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? ProfitAccountId { get; set; }
        public virtual Account ProfitAccount { get; set; }

        public long? LossAccountId { get; set; }
        public virtual Account LossAccount { get; set; }

        public decimal? AmountAuthorizedDiff { get; set; }

        /// <summary>
        /// Active in Point of Sale
        /// </summary>
        public bool? JournalUser { get; set; }

        /// <summary>
        /// Mã phát sinh trường hợp trả hàng
        /// </summary>
        public long? RefundSequenceId { get; set; }
        public virtual IRSequence RefundSequence { get; set; }

        /// <summary>
        /// Xác định có nên dùng riêng mã phát sinh trả hàng
        /// </summary>
        public bool? DedicatedRefund { get; set; }

        public long? BankAccountId { get; set; }
        public virtual ResPartnerBank BankAccount { get; set; }
    }
}
