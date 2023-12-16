using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class DanhDeLineXienMap : EntityTypeConfiguration<DanhDeLineXien>
    {
        public DanhDeLineXienMap()
        {
            ToTable("DanhDeLineXiens");
            HasKey(u => u.Id);
            Property(x => x.SoXien).IsRequired();

            HasOptional(x => x.Line)
                .WithMany(x => x.XienNumbers)
                .HasForeignKey(x => x.LineId)
                .WillCascadeOnDelete(true);
        }
    }
}
