using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class ResPartnerBankMap : EntityTypeConfiguration<ResPartnerBank>
    {
        public ResPartnerBankMap()
        {
            ToTable("ResPartnerBanks");
            HasKey(u => u.Id);
            Property(x => x.AccNumber).IsRequired();

            HasOptional(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.PartnerId);

            HasOptional(x => x.Bank)
                .WithMany()
                .HasForeignKey(x => x.BankId);

            HasOptional(x => x.Company)
               .WithMany()
               .HasForeignKey(x => x.CompanyId)
               .WillCascadeOnDelete(true);
        }
    }
}
