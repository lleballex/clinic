namespace Clinic.Model
{
    public class DoctorWorkDayFormModel
    {
        public string StartedAtMinutes { get; set; } = "";
        public string StartedAtHours { get; set; } = "";
        public string EndedAtMinutes { get; set; } = "";
        public string EndedAtHours { get; set; } = "";
        public required DayOfWeek WeekDay { get; set; }
        public bool IsWeekend { get; set; } = false;
    }
}
