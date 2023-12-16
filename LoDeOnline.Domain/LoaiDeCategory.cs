using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Nhóm loại đề: đánh lô, 3 càng...
    /// </summary>
    public class LoaiDeCategory : Entity
    {
        public LoaiDeCategory()
        {
            LoDeCategories = new List<LoDeCategory>();
        }

        public string Name { get; set; }

        public int? Sequence { get; set; }

        public virtual IList<LoDeCategory> LoDeCategories { get; set; }
    }
}
