using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("procedure")]
    public class Procedure
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("type_id")]
        [Required]
        public int TypeId { get; set; }

        [Column("assigned_appointment_id")]
        public int AssignedAppointmentId { get; set; }

        public ProcedureType? Type { get; set; }
        public Appointment? AssignedAppointment { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
