using DAL.Entities;

namespace DAL.Repositories
{
    public class DiagnosisRepository
    {
        private ClinicContext Context { get; set; }

        public DiagnosisRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<Diagnosis> FindAll(string? query = null)
        {
            return Context.Diagnosises
                .OrderBy(i => i.Name)
                .Where(i => (query == null || i.Name.Contains(query)))
                .ToList();
        }

        public void Create(Diagnosis data)
        {
            Context.Diagnosises.Add(data);
        }

        public void Update(Diagnosis data)
        {
            Context.ChangeTracker.Clear();
            Context.Diagnosises.Update(data);
        }

        public void Delete(Diagnosis data)
        {
            Context.Diagnosises.Remove(data);
        }
    }
}
