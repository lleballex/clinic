using DAL.Entities;

namespace DAL.Repositories
{
    public class HouseRepository
    {
        private ClinicContext Context;

        public HouseRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<House> FindAll(int? streetId = null)
        {
            return Context.Houses.Where(i => streetId == null | i.StreetId == streetId).ToList();
        }
    }
}
