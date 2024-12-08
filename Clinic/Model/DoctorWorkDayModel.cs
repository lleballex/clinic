namespace Clinic.Model
{
    public class DoctorWorkDayModel
    {
        public DayOfWeek WeekDay { get; set; }
        public bool IsWeekend { get; set; }
        public TimeOnly? StartedAt { get; set; }
        public TimeOnly? EndedAt { get; set; }
    }
}
