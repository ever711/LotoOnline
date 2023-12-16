using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountJournalMap : EntityTypeConfiguration<AccountJournal>
    {
        public AccountJournalMap()
        {
            ToTable("AccountJournals");
            HasKey(u => u.Id);

            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Type).IsRequired();
            this.Property(x => x.Code).IsRequired();

            HasRequired(x => x.Sequence)
                .WithMany()
                .HasForeignKey(x => x.SequenceId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.RefundSequence)
               .WithMany()
               .HasForeignKey(x => x.RefundSequenceId);

            HasOptional(x => x.DefaultCreditAccount)
                .WithMany()
                .HasForeignKey(x => x.DefaultCreditAccountId);

            HasOptional(x => x.DefaultDebitAccount)
                .WithMany()
                .HasForeignKey(x => x.DefaultDebitAccountId);

            HasOptional(x => x.ProfitAccount)
            .WithMany()
            .HasForeignKey(x => x.ProfitAccountId);

            HasOptional(x => x.LossAccount)
              .WithMany()
              .HasForeignKey(x => x.LossAccountId);

            HasOptional(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId);

            HasOptional(x => x.BankAccount)
             .WithMany()
             .HasForeignKey(x => x.BankAccountId);

            HasRequired(x => x.Company)
               .WithMany()
               .HasForeignKey(x => x.CompanyId)
               .WillCascadeOnDelete(false);
        }
    }
}
