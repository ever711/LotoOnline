using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.ToTable("Accounts");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Code).IsRequired();

            HasRequired(x => x.UserType)
                .WithMany()
                .HasForeignKey(x => x.UserTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .WillCascadeOnDelete(false);

            HasOptional(x => x.Currency)
             .WithMany()
             .HasForeignKey(x => x.CurrencyId);
        }
    }
}
