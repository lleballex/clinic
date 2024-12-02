using DAL.Entities;

namespace Clinic.Model
{
    public class DoctorFormModelWorkDay
    {
        public int StartedAtMinutes { get; set; }
        public int StartedAtHours { get; set; }
        public int EndedAtMinutes { get; set; }
        public int EndedAtHours { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public bool IsWeekend { get; set; }
    }

    public class DoctorFormModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public UserGender Gender { get; set; }
        public DateTime BornAt { get; set; }
        public TimeOnly AppointmentDuration { get; set; }
        public int SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        //public List<DoctorFormModelWorkDay> WorkDays { get; set; }
    }
}
