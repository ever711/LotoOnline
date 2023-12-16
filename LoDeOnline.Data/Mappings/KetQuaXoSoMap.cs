using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class KetQuaXoSoMap : EntityTypeConfiguration<KetQuaXoSo>
    {
        public KetQuaXoSoMap()
        {
            ToTable("KetQuaXoSos");
            HasKey(u => u.Id);

            HasRequired(x => x.DaiXS)
                .WithMany()
                .HasForeignKey(x => x.DaiXSId)
                .WillCascadeOnDelete(false);
        }
    }
}
