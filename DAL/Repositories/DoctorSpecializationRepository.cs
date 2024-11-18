using DAL.Entities;

namespace DAL.Repositories
{
    public class DoctorSpecializationRepository
    {
        private ClinicContext Context;

        public DoctorSpecializationRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<DoctorSpecialization> FindAll()
        {
            return Context.DoctorSpecializations.OrderByDescending(i => i.Name).ToList();
        }
    }
}
