using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class Company: Entity
    {
        public string Name { get; set; }

        public long PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public long CurrencyId { get; set; }
        public virtual ResCurrency Currency { get; set; }

        public long? AccountIncomeId { get; set; }
        public virtual Account AccountIncome { get; set; }

        public long? AccountExpenseId { get; set; }
        public virtual Account AccountExpense { get; set; }

        public long? AccountReceivableId { get; set; }
        public virtual Account AccountReceivable { get; set; }

        public string BankAccountCodePrefix { get; set; }

        public string CashAccountCodePrefix { get; set; }

        public int? AccountsCodeDigits { get; set; }

        /// <summary>
        /// Quy định số phút trước giờ mở số có thể ghi đề
        /// </summary>
        public int? SoPhutTruocKhiMoSo { get; set; }
    }
}
