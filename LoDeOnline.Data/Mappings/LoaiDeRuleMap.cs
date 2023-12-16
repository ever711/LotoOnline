using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class LoaiDeRuleMap : EntityTypeConfiguration<LoaiDeRule>
    {
        public LoaiDeRuleMap()
        {
            ToTable("LoaiDeRules");
            HasKey(u => u.Id);
            Property(x => x.GiaiDanh).IsRequired();
            Property(x => x.ViTriDanh).IsRequired();
            Property(x => x.KieuDanh).IsRequired();

            HasRequired(x => x.LoaiDe)
                .WithMany(x => x.Rules)
                .HasForeignKey(x => x.LoaiDeId);
        }
    }
}
