using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class LoaiDeMap : EntityTypeConfiguration<LoaiDe>
    {
        public LoaiDeMap()
        {
            ToTable("LoaiDes");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            HasOptional(x => x.LoDeCateg)
                .WithMany()
                .HasForeignKey(x => x.LoDeCategId);

            HasOptional(x => x.LoaiDeCateg)
              .WithMany()
              .HasForeignKey(x => x.LoaiDeCategId);
        }
    }
}
