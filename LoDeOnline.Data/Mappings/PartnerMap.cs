using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class PartnerMap : EntityTypeConfiguration<Partner>
    {
        public PartnerMap()
        {
            ToTable("Partners");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            this.HasOptional(x => x.Company)
             .WithMany()
             .HasForeignKey(x => x.CompanyId);
        }
    }
}
