using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public enum ProcedureStatus
    {
        Created, Finished, Canceled
    }

    [Table("procedure")]
    public class Procedure
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("type_id")]
        [Required]
        public int TypeId { get; set; }

        [Column("status")]
        [Required]
        public ProcedureStatus Status { get; set; }

        [Column("patient_id")]
        [Required]
        public int PatientId { get; set; }

        [Column("appointment_id")]
        public int? AppointmentId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public ProcedureType? Type { get; set; }
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
        public ProcedureResult? Result { get; set; }
    }
}
