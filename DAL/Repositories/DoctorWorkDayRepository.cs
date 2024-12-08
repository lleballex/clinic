using DAL.Entities;

namespace DAL.Repositories
{
    public class DoctorWorkDayRepository
    {
        private ClinicContext Context;

        public DoctorWorkDayRepository(ClinicContext context)
        {
            Context = context;
        }

        public void Create(DoctorWorkDay data)
        {
            Context.DoctorWorkDays.Add(data);
        }

        public void Update(DoctorWorkDay data)
        {
            Context.ChangeTracker.Clear();
            Context.DoctorWorkDays.Update(data);
        }

        public void Delete(DoctorWorkDay data)
        {
            // TODO: fix and remove
            Context.ChangeTracker.Clear();
            Context.DoctorWorkDays.Remove(data);
        }
    }
}
