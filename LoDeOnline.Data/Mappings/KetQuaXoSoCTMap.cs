using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class KetQuaXoSoCTMap : EntityTypeConfiguration<KetQuaXoSoCT>
    {
        public KetQuaXoSoCTMap()
        {
            ToTable("KetQuaXoSoCTs");
            HasKey(u => u.Id);

            HasRequired(x => x.KQXS)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.KQXSId);
        }
    }
}
