using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class DaiXoSoRuleMap : EntityTypeConfiguration<DaiXoSoRule>
    {
        public DaiXoSoRuleMap()
        {
            ToTable("DaiXoSoRules");
            HasKey(u => u.Id);
            Property(x => x.Thu).IsRequired();

            HasRequired(x => x.Dai)
                .WithMany(x => x.Rules)
                .HasForeignKey(x => x.DaiId);
        }
    }
}
