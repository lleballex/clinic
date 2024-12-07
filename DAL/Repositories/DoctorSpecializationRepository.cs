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

        public List<DoctorSpecialization> FindAll(string? query = null)
        {
            return Context.DoctorSpecializations
                .OrderBy(i => i.Name)
                .Where(i => (query == null || i.Name.Contains(query)))
                .ToList();
        }

        public void Create(DoctorSpecialization data)
        {
            Context.DoctorSpecializations.Add(data);
        }

        public void Update(DoctorSpecialization data)
        {
            Context.ChangeTracker.Clear();
            Context.DoctorSpecializations.Update(data);
        }

        public void Delete(DoctorSpecialization data)
        {
            Context.DoctorSpecializations.Remove(data);
        }
    }
}
