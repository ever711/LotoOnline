using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class AccountJournalDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public long CompanyId { get; set; }

        public string BankAccNumber { get; set; }

        public long? BankId { get; set; }
        public ResBankDTO Bank { get; set; }
        public string BankName { get; set; }

        public string Code { get; set; }

        public long? DefaultDebitAccountId { get; set; }

        public long? DefaultCreditAccountId { get; set; }

        public long? CurrencyId { get; set; }

        public long? SequenceId { get; set; }
    }
}
