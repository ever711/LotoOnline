using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class IRModelAccessMap : EntityTypeConfiguration<IRModelAccess>
    {
        public IRModelAccessMap()
        {
            this.ToTable("IRModelAccesses");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();

            HasRequired(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId);

            HasOptional(x => x.Group)
              .WithMany(x => x.ModelAccesses)
              .HasForeignKey(x => x.GroupId)
              .WillCascadeOnDelete(true);
        }
    }
}
