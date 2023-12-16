using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class IRModelMap : EntityTypeConfiguration<IRModel>
    {
        public IRModelMap()
        {
            this.ToTable("IRModels");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();
            Property(x => x.Model).IsRequired();
        }
    }
}
