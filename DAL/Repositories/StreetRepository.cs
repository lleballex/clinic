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

        public List<Street> FindAll(string? query = null)
        {
            return Context.Streets
                .OrderBy(i => i.Name)
                .Where(i => (query == null || i.Name.Contains(query)))
                .ToList();
        }

        public void Create(Street data)
        {
            Context.Streets.Add(data);
        }

        public void Update(Street data)
        {
            Context.ChangeTracker.Clear();
            Context.Streets.Update(data);
        }

        public void Delete(Street data)
        {
            Context.Streets.Remove(data);
        }
    }
}
