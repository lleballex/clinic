using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public enum AppointmentStatus
    {
        Created, Finished, Canceled
    }

    [Table("appointment")]
    public class Appointment
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("datetime")]
        [Required]
        public DateTime Datetime { get; set; }

        [Column("status")]
        [Required]
        public AppointmentStatus Status { get; set; }

        [Column("patient_id")]
        [Required]
        public int PatientId { get; set; }

        [Column("doctor_id")]
        [Required]
        public int DoctorId { get; set; }

        [Column("procedure_id")]
        public int? ProcedureId { get; set; }

        public Patient? Patient { get; set; }
        public DoctorProfile? Doctor { get; set; }
        public AppointmentResult? Result { get; set; }

        public Procedure? Procedure { get; set; }
        public ICollection<Procedure>? AssignedProcedures { get; set; }
    }
}
