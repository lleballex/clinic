using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities 
{
    [Table("house")]
    public class House
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("number")]
        [Required]
        public string Number { get; set; }

        [Column("street_id")]
        [Required]
        public int StreetId { get; set; }

        [Column("department_id")]
        [Required]
        public int DepartmentId { get; set; }

        public Street? Street { get; set; }
        public Department? Department { get; set; }
    }
}
