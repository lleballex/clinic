using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("appointment_result")]
    public class AppointmentResult
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("diagnosis_description")]
        public string? DiagnosisDescription { get; set; }

        [Column("recommendations")]
        public string? Recommendations { get; set; }

        [Column("appointment_id")]
        [Required]
        public int AppointmentId { get; set; }

        [Column("diagnosis_id")]
        [Required]
        public int DiagnosisId { get; set; }
        
        public Appointment? Appointment { get; set; }
        public Diagnosis? Diagnosis { get; set; }
    }
}
