using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private ClinicContext Context;

        public UserRepository(ClinicContext context)
        {
            Context = context;
        }

        public User? FindByAuthData(string email, string password)
        {
            return Context.Users.Include(i => i.Doctor).FirstOrDefault(i => i.Email == email && i.Password == password);
        }
    }
}
