using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class ResCurrency: Entity
    {
        public ResCurrency()
        {
            Rounding = 0.01M;
            Active = true;
            Position = "after";
        }

        public string Name { get; set; }

        public decimal? Rounding { get; set; }

        public string Symbol { get; set; }

        public bool? Active { get; set; }

        public string Position { get; set; }

        public int DecimalPlaces
        {
            get
            {
                if (Rounding > 0 && Rounding < 1)
                {
                    return (int)Math.Ceiling(Math.Log10((double)(1 / Rounding)));
                }

                return 0;
            }
        }
    }
}
