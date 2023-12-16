using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class DaiXoSoMap: EntityTypeConfiguration<DaiXoSo>
    {
        public DaiXoSoMap()
        {
            ToTable("DaiXoSos");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();
            Property(x => x.Code).IsRequired();

            HasOptional(x => x.Mien)
                .WithMany()
                .HasForeignKey(x => x.MienId);
        }
    }
}
