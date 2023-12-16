using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class AccountJournalService : Service<AccountJournal>
    {
        public AccountJournalService(IRepository<AccountJournal> repository)
            : base(repository)
        {
        }

        public void Create(AccountJournal vals, string acc_number = "", long? bank_id = null)
        {
            var company_id = vals.CompanyId;
            if (vals.Type == "bank" || vals.Type == "cash")
            {
                //  # If no code provided, loop to find next available journal code
                if (string.IsNullOrEmpty(vals.Code))
                {
                    var journal_code_base = vals.Type == "cash" ? "CASH" : "BNK";
                    var journalObj = DependencyResolver.Current.GetService<AccountJournalService>();
                    var journals = journalObj.Search(x => x.Code.Contains(journal_code_base) && x.CompanyId == company_id);
                    for (var num = 1; num < 100; num++)
                    {
                        var journal_code = journal_code_base + num.ToString();
                        if (!journals.Any(x => x.Code == journal_code))
                        {
                            vals.Code = journal_code;
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(vals.Code))
                        throw new Exception("Cannot generate an unused journal code. Please fill the 'Shortcode' field.");
                }

                //Create a default debit/credit account if not given
                var default_account = vals.DefaultDebitAccount ?? vals.DefaultCreditAccount;
                if (default_account == null)
                {
                    var companyObj = DependencyResolver.Current.GetService<CompanyService>();
                    var accountObj = DependencyResolver.Current.GetService<AccountService>();
                    var company = companyObj.GetById(company_id);
                    var account_vals = _PrepareLiquidityAccount(vals.Name, company, vals.CurrencyId, vals.Type);
                    default_account = accountObj.Create(account_vals);
                    vals.DefaultDebitAccountId = default_account.Id;
                    vals.DefaultCreditAccountId = default_account.Id;
                }
            }

            if (vals.SequenceId == 0)
            {
                vals.SequenceId = _CreateSequence(vals).Id;
            }
            var journal = Insert(vals);
            //Create the bank_account_id if necessary
            if (journal.Type == "bank" && !journal.BankAccountId.HasValue && !string.IsNullOrEmpty(acc_number))
            {
                SetBankAccount(journal, acc_number, bank_id);
                SaveChanges();
            }
        }

        public void Write(AccountJournal self, string acc_number, long? bank_id)
        {
            var partnerBnkObj = DependencyResolver.Current.GetService<ResPartnerBankService>();
            if (self.Type == "bank" && !self.BankAccountId.HasValue && !string.IsNullOrEmpty(acc_number))
            {
                SetBankAccount(self, acc_number, bank_id);
                SaveChanges();
            }
            else if (self.Type == "bank" && self.BankAccount != null && !string.IsNullOrEmpty(acc_number))
            {
                self.BankAccount.AccNumber = acc_number;
                self.BankAccount.BankId = bank_id;
                partnerBnkObj.Write(self.BankAccount);
            }

            Update(self);
        }

        public void Unlink(AccountJournal self)
        {
            if (self.BankAccount != null)
            {
                var partnerBnkObj = DependencyResolver.Current.GetService<ResPartnerBankService>();
                partnerBnkObj.Delete(self.BankAccount);
            }
            Delete(self);
        }

        private void SetBankAccount(AccountJournal self, string acc_number, long? bank_id = null)
        {
            var partnerBankObj = DependencyResolver.Current.GetService<ResPartnerBankService>();
            self.BankAccountId = partnerBankObj.Create(new ResPartnerBank
            {
                AccNumber = acc_number,
                BankId = bank_id,
                CompanyId = self.CompanyId,
                PartnerId = self.Company.PartnerId,
            }).Id;
        }

        private IRSequence _CreateSequence(AccountJournal self, bool refund = false)
        {
            var seqObj = DependencyResolver.Current.GetService<IRSequenceService>();
            var prefix = _GetSequencePrefix(self.Code, refund: refund);
            var seq = new IRSequence
            {
                Name = self.Name,
                Prefix = prefix,
                Padding = 4,
                NumberIncrement = 1,
                NumberNext = 1,
                CompanyId = self.CompanyId,
            };

            seqObj.Insert(seq);
            return seq;
        }

        public string _GetSequencePrefix(string code, bool refund = false)
        {
            var prefix = code.ToUpper();
            if (refund)
                prefix = "R" + prefix;
            return prefix + "/{yyyy}/";
        }

        private Account _PrepareLiquidityAccount(string name, Company company, long? currency_id, string type)
        {
            var code_digits = company.AccountsCodeDigits ?? 0;
            string account_code_prefix = "";
            if (type == "bank")
            {
                account_code_prefix = company.BankAccountCodePrefix ?? "";
            }
            else
            {
                account_code_prefix = company.CashAccountCodePrefix ?? (company.BankAccountCodePrefix ?? "");
            }

            string new_code = "";
            for (var num = 1; num < 100; num++)
            {
                var fm = "{0, " + (code_digits - 1) + "}";
                new_code = account_code_prefix + (code_digits >= 1 ? new String('0', code_digits - 1) : "") + num;
                var accountObj = DependencyResolver.Current.GetService<AccountService>();
                var rec = accountObj.Search(x => x.Code == new_code && x.CompanyId == company.Id).FirstOrDefault();
                if (rec == null)
                    break;
                new_code = "";
            }

            if (string.IsNullOrEmpty(new_code))
                throw new Exception("Cannot generate an unused account code.");
            var modelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
            var liquidity_type = (AccountType)modelDataObj.GetRef("account.data_account_type_liquidity");
            if (liquidity_type == null)
                throw new Exception("Không tìm thấy nhóm tài khoản");

            return new Account
            {
                Name = name,
                Code = new_code,
                CurrencyId = currency_id,
                UserTypeId = liquidity_type.Id,
                UserType = liquidity_type,
                CompanyId = company.Id
            };
        }
    }
}
