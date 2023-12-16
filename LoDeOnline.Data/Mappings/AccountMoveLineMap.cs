using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountMoveLineMap : EntityTypeConfiguration<AccountMoveLine>
    {
        public AccountMoveLineMap()
        {
            this.ToTable("AccountMoveLines");
            this.HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            this.HasRequired(x => x.Journal)
           .WithMany()
           .HasForeignKey(x => x.JournalId)
           .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Account)
              .WithMany()
              .HasForeignKey(x => x.AccountId)
              .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Move)
              .WithMany(x => x.MoveLines)
              .HasForeignKey(x => x.MoveId)
              .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.PartnerId);

            HasOptional(x => x.Company)
             .WithMany()
             .HasForeignKey(x => x.CompanyId);

            HasOptional(x => x.DanhDe)
             .WithMany()
             .HasForeignKey(x => x.DanhDeId);

            HasOptional(x => x.FullReconcile)
               .WithMany(x => x.ReconciledLines)
               .HasForeignKey(x => x.FullReconcileId);

            HasOptional(x => x.Payment)
             .WithMany(x => x.MoveLines)
             .HasForeignKey(x => x.PaymentId);
        }
    }
}
