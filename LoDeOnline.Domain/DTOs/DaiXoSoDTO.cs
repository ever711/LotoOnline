using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Domain.DTOs
{
    public class DaiXoSoDTO
    {
        public DaiXoSoDTO()
        {
            Rules = new List<DaiXoSoRuleDTO>();
        }
        public long Id { get; set; }

        /// <summary>
        /// Tên: Bình Định
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mã: xsbdi
        /// </summary>
        public string Code { get; set; }

        public long? MienId { get; set; }
        public LoDeCategoryDTO Mien { get; set; }

        public IEnumerable<DaiXoSoRuleDTO> Rules { get; set; }
    }
}