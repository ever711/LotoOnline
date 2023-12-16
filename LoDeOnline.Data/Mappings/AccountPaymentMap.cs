using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountPaymentMap : EntityTypeConfiguration<AccountPayment>
    {
        public AccountPaymentMap()
        {
            this.ToTable("AccountPayments");
            this.HasKey(u => u.Id);
            this.Property(x => x.PaymentType).IsRequired();

            HasRequired(x => x.Journal)
                .WithMany()
                .HasForeignKey(x => x.JournalId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId);

            HasOptional(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.PartnerId);
        }
    }
}
