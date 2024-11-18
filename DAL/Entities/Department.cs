using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("department")]
    public class Department
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("number")]
        [Required]
        // TODO: unique
        public int Number { get; set; }
        
        public ICollection<DoctorProfile>? Doctors { get; set; }
    }
}
