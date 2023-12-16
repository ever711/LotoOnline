using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class IRRuleMap : EntityTypeConfiguration<IRRule>
    {
        public IRRuleMap()
        {
            this.ToTable("IRRules");
            this.HasKey(u => u.Id);

            HasRequired(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Groups)
                .WithMany(x => x.Rules)
                .Map(x => x.ToTable("RuleGroupRel").MapLeftKey("RuleGroupId").MapRightKey("GroupId"));
        }
    }
}
