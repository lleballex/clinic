using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("procedurer_profile")]
    public class ProcedurerProfile
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }
        public ICollection<ProcedureType>? procedureTypes { get; set; }
    }
}
