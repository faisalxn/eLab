using System.ComponentModel.DataAnnotations.Schema;

namespace eLab.Data
{
    [Table("Tests")]
    public class Test
    {
        public int id { get; set; }
        public string testName { get; set; }
        public int price { get; set; }

    }
}
