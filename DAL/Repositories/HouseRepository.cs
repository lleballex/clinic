using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class HouseRepository
    {
        private ClinicContext Context;

        public HouseRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<House> FindAll(int? streetId = null, string? query = null)
        {
            return Context.Houses
                .Include(i => i.Street)
                .Include(i => i.Department)
                .OrderBy(i => i.Number)
                .Where(i => (streetId == null || i.StreetId == streetId) && (query == null || i.Number.Contains(query)))
                .ToList();
        }

        public void Create(House data)
        {
            Context.Houses.Add(data);
        }

        public void Update(House data)
        {
            Context.ChangeTracker.Clear();
            Context.Houses.Update(data);
        }

        public void Delete(House data)
        {
            Context.Houses.Remove(data);
        }
    }
}
