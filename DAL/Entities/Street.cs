using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Street
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }
    }
}
