using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData.Query;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Đài xổ số: Vĩnh Long, An Giang....
    /// </summary>
    public class DaiXoSo : Entity
    {
        public DaiXoSo()
        {
            Rules = new List<DaiXoSoRule>();
        }
        /// <summary>
        /// Tên: Bình Định
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mã: xsbdi
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Miền xổ số
        /// </summary>
        public long? MienId { get; set; }
        public LoDeCategory Mien { get; set; }

        public virtual IList<DaiXoSoRule> Rules { get; set; }
    }
}
