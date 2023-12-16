using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoDeOnline.Data.Mappings
{
    public class TinTucMap : EntityTypeConfiguration<TinTuc>
    {
        public TinTucMap()
        {
            ToTable("TinTucs");
            HasKey(u => u.MaTin);
            Property(x => x.MaTin).IsRequired();
        }
    }
}
