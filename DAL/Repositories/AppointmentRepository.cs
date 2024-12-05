using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AppointmentRepository
    {
        private ClinicContext Context;

        public AppointmentRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<Appointment> FindAll(int? patientId = null, int? doctorId = null, DateOnly? date = null)
        {
            return Context.Appointments
                .Include(i => i.Result)
                .ThenInclude(i => i.Diagnosis)
                .Include(i => i.Doctor)
                .ThenInclude(i => i.Specialization)
                .Include(i => i.Patient)
                .Where(i => (patientId == null || i.PatientId == patientId) && (doctorId == null || i.DoctorId == doctorId) && (date == null || DateOnly.FromDateTime(i.Datetime.ToLocalTime()) == date))
                .OrderByDescending(i => i.Datetime)
                .ToList();
        }

        // TODO: remove
        public List<Appointment> FindByPatient(int patientId)
        {
            return Context.Appointments
                .Include(i => i.Result)
                .ThenInclude(i => i.Diagnosis)
                .Include(i => i.Doctor)
                .ThenInclude(i => i.Specialization)
                .Where(i => i.PatientId == patientId)
                .OrderByDescending(i => i.Datetime)
                .ToList();
        }

        public void Create(Appointment data)
        {
            Context.Appointments.Add(data);
        }

        public void Update(Appointment data)
        {
            Context.Attach(data).State = EntityState.Modified;
        }
    }
}
