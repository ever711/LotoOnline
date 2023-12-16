using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class IRModuleCategoryMap : EntityTypeConfiguration<IRModuleCategory>
    {
        public IRModuleCategoryMap()
        {
            this.ToTable("IRModuleCategories");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();
        }
    }
}
