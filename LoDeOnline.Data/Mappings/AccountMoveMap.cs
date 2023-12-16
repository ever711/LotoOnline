using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountMoveMap : EntityTypeConfiguration<AccountMove>
    {
        public AccountMoveMap()
        {
            this.ToTable("AccountMoves");
            this.HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();
            Property(x => x.State).IsRequired();

            this.HasRequired(x => x.Journal)
           .WithMany()
           .HasForeignKey(x => x.JournalId)
           .WillCascadeOnDelete(false);

            HasOptional(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId);

            HasOptional(x => x.Company)
             .WithMany()
             .HasForeignKey(x => x.CompanyId);
        }
    }
}
