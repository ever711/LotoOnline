﻿using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data.Mappings
{
    public class LoDeCategoryMap : EntityTypeConfiguration<LoDeCategory>
    {
        public LoDeCategoryMap()
        {
            ToTable("LoDeCategories");
            HasKey(u => u.Id);
            Property(x => x.Name).IsRequired();
        }
    }
}