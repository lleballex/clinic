using DAL.Entities;

namespace DAL.Repositories
{
    public class StreetRepository
    {
        private ClinicContext Context;
        
        public StreetRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<Street> FindAll()
        {
            return Context.Streets.ToList();
        }
    }
}
