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
    }
}
