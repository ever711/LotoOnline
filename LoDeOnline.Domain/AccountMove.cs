using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountMove : Entity
    {
        public AccountMove()
        {
            Name = "/";
            State = "draft";
            Date = DateTime.Today;
            MoveLines = new List<AccountMoveLine>();
        }

        public AccountMove(AccountMove move)
        {
            Name = "/";
            Date = move.Date;
            JournalId = move.JournalId;
            Journal = move.Journal;
            CurrencyId = move.CurrencyId;
            Currency = move.Currency;
            State = "draft";
            PartnerId = move.PartnerId;
            Partner = move.Partner;
            Amount = move.Amount;
            CompanyId = move.CompanyId;
            Company = move.Company;
        }

        /// <summary>
        /// Number 
        /// </summary>
        public string Name { get; set; }

        public string State { get; set; }

        public string Ref { get; set; }

        public long JournalId { get; set; }
        public virtual AccountJournal Journal { get; set; }

        /// <summary>
        /// Internal Note
        /// </summary>
        public string Narration { get; set; }

        public DateTime Date { get; set; }

        public long? PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public virtual IList<AccountMoveLine> MoveLines { get; set; }

        public long? CurrencyId { get; set; }
        public virtual ResCurrency Currency { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public decimal? Amount { get; set; }
    }
}
