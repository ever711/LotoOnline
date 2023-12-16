using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountMoveLine: Entity
    {
        public AccountMoveLine()
        {
            Credit = 0;
            Debit = 0;
            Balance = 0;
            DateMaturity = DateTime.Today;
            MatchedDebits = new List<AccountPartialReconcile>();
            MatchedCredits = new List<AccountPartialReconcile>();
        }

        public AccountMoveLine(AccountMoveLine line)
        {
            Name = line.Name;
            Quantity = line.Quantity;
            Debit = line.Debit;
            Credit = line.Credit;
            CompanyId = line.CompanyId;
            Company = line.Company;
            Account = line.Account;
            AccountId = line.AccountId;
            MoveId = line.MoveId;
            Ref = line.Ref;
            JournalId = line.JournalId;
            Date = line.Date;
        }

        public string Name { get; set; }

        public decimal? Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ref { get; set; }

        public long JournalId { get; set; }
        public virtual AccountJournal Journal { get; set; }

        /// <summary>
        /// Ngày đáo hạn
        /// </summary>
        public DateTime DateMaturity { get; set; }

        public long? PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public DateTime Date { get; set; }

        public long MoveId { get; set; }
        public virtual AccountMove Move { get; set; }

        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Ghi nợ
        /// </summary>
        public decimal? Credit { get; set; }

        /// <summary>
        /// Trả trước
        /// </summary>
        public decimal? Debit { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }

        public decimal? AmountResidual { get; set; }

        public decimal? Balance { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public bool? Reconciled { get; set; }

        public long? DanhDeId { get; set; }
        public virtual DanhDe DanhDe { get; set; }

        public long? FullReconcileId { get; set; }
        public virtual AccountFullReconcile FullReconcile { get; set; }

        public virtual IList<AccountPartialReconcile> MatchedDebits { get; set; }

        public virtual IList<AccountPartialReconcile> MatchedCredits { get; set; }

        public long? PaymentId { get; set; }
        public virtual AccountPayment Payment { get; set; }
    }
}
