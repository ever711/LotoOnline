using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Nhóm lô đề: lô đề miền bắc, lô đề miền trung....
    /// </summary>
    public class LoDeCategory: Entity
    {
        public LoDeCategory()
        {
            LoaiDeCategories = new List<LoaiDeCategory>();
        }

        public string Name { get; set; }

        public int? Sequence { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public virtual IList<LoaiDeCategory> LoaiDeCategories { get; set; }
    }
}
