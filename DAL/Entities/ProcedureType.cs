using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("procedure_type")]
    public class ProcedureType
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        public ICollection<ProcedurerProfile>? Procedurers { get; set; }
    }
}
