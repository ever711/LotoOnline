using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class KetQuaXoSo: Entity
    {
        public KetQuaXoSo()
        {
            Lines = new List<KetQuaXoSoCT>();
        }

        /// <summary>
        /// Mô tả: Xổ số An giang ngày 4/6/2018
        /// </summary>
        public string Name { get; set; }

        public DateTime Ngay { get; set; }

        public long DaiXSId { get; set; }
        public virtual DaiXoSo DaiXS { get; set; }

        public virtual IList<KetQuaXoSoCT> Lines { get; set; }
    }
}
