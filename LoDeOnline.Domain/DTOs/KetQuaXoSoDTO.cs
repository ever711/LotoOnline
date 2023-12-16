using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class KetQuaXoSoDTO
    {
        public KetQuaXoSoDTO()
        {
            Lines = new List<KetQuaXoSoCTDTO>();
        }

        public long Id { get; set; }

        /// <summary>
        /// Mô tả: Xổ số An giang ngày 4/6/2018
        /// </summary>
        public string Name { get; set; }

        public DateTime Ngay { get; set; }

        public long DaiXSId { get; set; }
        public DaiXoSoDTO DaiXS { get; set; }
        public string DaiXSName { get; set; }

        public IEnumerable<KetQuaXoSoCTDTO> Lines { get; set; }
    }
}
