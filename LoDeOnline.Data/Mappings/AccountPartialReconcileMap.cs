using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class AccountPartialReconcileMap : EntityTypeConfiguration<AccountPartialReconcile>
    {
        public AccountPartialReconcileMap()
        {
            this.ToTable("AccountPartialReconciles");
            this.HasKey(u => u.Id);

            HasRequired(x => x.CreditMove)
                .WithMany(x => x.MatchedDebits)
                .HasForeignKey(x => x.CreditMoveId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.DebitMove)
             .WithMany(x => x.MatchedCredits)
             .HasForeignKey(x => x.DebitMoveId)
             .WillCascadeOnDelete(false);

            HasOptional(x => x.Company)
             .WithMany()
             .HasForeignKey(x => x.CompanyId);

            HasOptional(x => x.FullReconcile)
           .WithMany(x => x.PartialReconciles)
           .HasForeignKey(x => x.FullReconcileId);
        }
    }
}
