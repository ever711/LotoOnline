using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();

            HasRequired(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.PartnerId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Company)
               .WithMany()
               .HasForeignKey(x => x.CompanyId);
        }
    }
}
