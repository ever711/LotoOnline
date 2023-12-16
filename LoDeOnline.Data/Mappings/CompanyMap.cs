using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            ToTable("Companies");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            this.HasRequired(x => x.Partner)
               .WithMany()
               .HasForeignKey(x => x.PartnerId)
               .WillCascadeOnDelete(false);

            HasRequired(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.AccountIncome)
                .WithMany()
                .HasForeignKey(x => x.AccountIncomeId);

            HasOptional(x => x.AccountExpense)
             .WithMany()
             .HasForeignKey(x => x.AccountExpenseId);

            HasOptional(x => x.AccountReceivable)
               .WithMany()
               .HasForeignKey(x => x.AccountReceivableId);
        }
    }
}
