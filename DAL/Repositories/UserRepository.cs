using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

        public void Create(User data)
        {
            Context.Users.Add(data);
        }

        public void Delete(User data)
        {
            Context.Users.Remove(data);
        }
    }
}
