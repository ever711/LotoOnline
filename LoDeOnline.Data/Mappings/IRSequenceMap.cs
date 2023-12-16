using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class IRSequenceMap : EntityTypeConfiguration<IRSequence>
    {
        public IRSequenceMap()
        {
            this.ToTable("IRSequences");
            this.HasKey(u => u.Id);
            this.Property(x => x.Name).IsRequired();

            HasOptional(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId);
        }
    }
}
