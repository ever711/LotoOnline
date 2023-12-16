using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class ResGroupMap : EntityTypeConfiguration<ResGroup>
    {
        public ResGroupMap()
        {
            this.ToTable("ResGroups");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();

            HasOptional(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);

            HasMany(x => x.Users).WithMany(x => x.Groups)
                .Map(x => x.ToTable("ResGroupsUsersRel").MapLeftKey("GId").MapRightKey("UId"));

            HasMany(x => x.Implieds)
                .WithMany()
                .Map(x => x.ToTable("ResGroupsImpliedRel").MapLeftKey("GId").MapRightKey("HId"));
        }
    }
}
