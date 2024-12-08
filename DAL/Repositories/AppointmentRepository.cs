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

        public List<Appointment> FindAll(int? patientId = null, int? doctorId = null, DateOnly? date = null, AppointmentStatus? status = null)
        {
            return Context.Appointments
                .Include(i => i.Result)
                .ThenInclude(i => i.Diagnosis)
                .Include(i => i.Doctor)
                .ThenInclude(i => i.Specialization)
                .Include(i => i.Patient)
                .Include(i => i.AssignedProcedures)
                .ThenInclude(i => i.Type)
                .Include(i => i.AssignedProcedures)
                .ThenInclude(i => i.Appointment)
                .Include(i => i.Procedure)
                .ThenInclude(i => i.Type)
                .Where(i => (patientId == null || i.PatientId == patientId) && (doctorId == null || i.DoctorId == doctorId) &&
                            (date == null || DateOnly.FromDateTime(i.Datetime.ToLocalTime()) == date) &&
                            (status == null || i.Status == status))
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
