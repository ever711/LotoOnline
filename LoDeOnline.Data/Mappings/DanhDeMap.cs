using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class DanhDeMap : EntityTypeConfiguration<DanhDe>
    {
        public DanhDeMap()
        {
            ToTable("DanhDes");
            HasKey(u => u.Id);

            HasRequired(x => x.Dai)
                .WithMany()
                .HasForeignKey(x => x.DaiId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Partner)
              .WithMany()
              .HasForeignKey(x => x.PartnerId)
              .WillCascadeOnDelete(false);

            HasRequired(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Move)
                .WithMany()
                .HasForeignKey(x => x.MoveId);

            HasOptional(x => x.TrungMove)
                .WithMany()
                .HasForeignKey(x => x.TrungMoveId);
        }
    }
}
