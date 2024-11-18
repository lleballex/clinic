using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DoctorProfileRepository
    {
        private ClinicContext Context;

        public DoctorProfileRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<DoctorProfile> FindAll(int? departmentId = null, int? specializationId = null, string? query = null)
        {
            return Context.DoctorProfiles
                .Include(i => i.User)
                .Include(i => i.Specialization)
                .Include(i => i.Department)
                .Include(i => i.WorkDays)
                .Where(i =>
                    (departmentId == null || i.DepartmentId == null || i.DepartmentId == departmentId) &&
                    (specializationId == null || i.SpecializationId == specializationId) &&
                    (query == null || (i.User.Surname + " " + i.User.Name + " " + i.User.Patronymic).Contains(query)))
                .OrderBy(i => i.User.Surname)
                .ToList();
        }

        public void Create(DoctorProfile data)
        {
            Context.DoctorProfiles.Add(data);
        }

        // TODO: move somewhere
        public List<TimeOnly> FindFreeTimeSlots(int doctorId, DateOnly date)
        {
            var workDay = Context.DoctorWorkDays.Include(i => i.Doctor).Where(i => i.DoctorId == doctorId && i.WeekDay == date.DayOfWeek).FirstOrDefault();

            if (workDay == null) return [];

            var appointments = Context.Appointments.Where(i => i.DoctorId == doctorId && DateOnly.FromDateTime(i.Datetime) == date).ToList();

            var curDate = workDay.StartedAt;
            var slots = new List<TimeOnly>();

            while (curDate < workDay.EndedAt)
            {
                if (!appointments.Any(i => TimeOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(i.Datetime, TimeZoneInfo.Local)).Equals(curDate)))
                {
                    slots.Add(curDate);
                }
                curDate = curDate.Add(workDay.Doctor.AppointmentDuration.ToTimeSpan());
            }

            return slots;
        }
    }
}
