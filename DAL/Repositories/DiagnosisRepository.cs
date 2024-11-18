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

        public List<Diagnosis> FindAll()
        {
            return Context.Diagnosises.OrderByDescending(i => i.Name).ToList();
        }
    }
}
