using eLab.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace eLab.Data
{
    [Table("LabTest")]
    public class LabTest : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Preparation { get; set; }
    }

}
