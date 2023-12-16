using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public abstract class Entity
    {
        public Entity()
        {
            DateCreated = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }

        public virtual ApplicationUser WritedBy { get; set; }

        [ForeignKey("WritedBy")]
        public string WritedById { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
