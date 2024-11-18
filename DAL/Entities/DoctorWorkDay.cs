using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("doctor_work_day")]
    public class DoctorWorkDay
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("week_day")]
        [Required]
        public DayOfWeek WeekDay { get; set; }

        [Column("started_at")]
        [Required]
        public TimeOnly StartedAt { get; set; }

        [Column("ended_at")]
        [Required]
        public TimeOnly EndedAt { get; set; }

        [Column("doctor_id")]
        [Required]
        public int DoctorId { get; set; }

        public DoctorProfile? Doctor { get; set; }
    }
}
