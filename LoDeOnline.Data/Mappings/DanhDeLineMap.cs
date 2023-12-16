using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class DanhDeLineMap : EntityTypeConfiguration<DanhDeLine>
    {
        public DanhDeLineMap()
        {
            ToTable("DanhDeLines");
            HasKey(u => u.Id);

            HasOptional(x => x.Dai)
                .WithMany()
                .HasForeignKey(x => x.DaiId);

            HasRequired(x => x.LoaiDe)
                .WithMany()
                .HasForeignKey(x => x.LoaiDeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.DanhDe)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.DanhDeId);
        }
    }
}
