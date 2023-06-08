using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLab.Data.Entities
{
    public class Audit
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public ApplicationUser UpdatedByUser { get; set; }
    }
}
