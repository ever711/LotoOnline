using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Models
{
    public class LoDeCategoryTopMenu
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }
    }

    public class LoDeCategoryShow
    {
        public long Id { get; set; }
        /// <summary>
        /// loại đề
        /// </summary>
        public IList<LoaiDeCategorySimple> LoaiDeCategs { get; set; }

        public DateTime Date { get; set; }

        public IList<DaiXoSoViewModel> Dais { get; set; }

    }
}