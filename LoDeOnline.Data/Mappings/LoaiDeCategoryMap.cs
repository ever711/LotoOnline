using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class LoaiDeCategoryMap : EntityTypeConfiguration<LoaiDeCategory>
    {
        public LoaiDeCategoryMap()
        {
            ToTable("LoaiDeCategories");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            HasMany(x => x.LoDeCategories)
                .WithMany(x => x.LoaiDeCategories)
                .Map(x => x.ToTable("LoaiDeCategoryLoDeCategoryRel").MapLeftKey("LoaiDeCategId").MapRightKey("LoDeCategId"));
        }
    }
}
