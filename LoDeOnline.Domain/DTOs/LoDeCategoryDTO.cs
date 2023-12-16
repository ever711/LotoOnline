﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class LoDeCategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int? Sequence { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }
}