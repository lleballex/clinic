using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("doctor_specialization")]
    public class DoctorSpecialization
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }
    }
}
