using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("procedure_result")]
    public class ProcedureResult
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("procedure_id")]
        [Required]
        public int ProcedureId { get; set; }

        [Column("procedurer_id")]
        [Required]
        public int ProcedurerId { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        public Procedure? Procedure { get; set; }
        public ProcedurerProfile? Procedurer { get; set; }
    }
}
