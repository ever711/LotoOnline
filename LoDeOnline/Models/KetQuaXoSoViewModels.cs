using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoDeOnline.Models
{
    public class KetQuaXoSoTable
    {
        public string Name { get; set; }

        public IEnumerable<KetQuaXoSoTableRow> Rows { get; set; }
    }

    public class KetQuaXoSoTableRow
    {
        public string Giai { get; set; }

        public string SoTrungs { get; set; }
    }
}