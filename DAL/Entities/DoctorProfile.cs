using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("doctor_profile")]
    public class DoctorProfile
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("appointment_duration")]
        [Required]
        public TimeOnly AppointmentDuration { get; set; }

        [Column("specialization_id")]
        [Required]
        public int SpecializationId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("department_id")]
        public int? DepartmentId { get; set; }

        public DoctorSpecialization? Specialization { get; set; }
        public User? User { get; set; }
        public Department? Department { get; set; }
        public ICollection<DoctorWorkDay>? WorkDays { get; set; }
    }
}
