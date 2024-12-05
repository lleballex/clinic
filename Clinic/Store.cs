using DAL.Entities;
using DAL.Repositories;

namespace Clinic
{
    public class Store
    {
        private static Store? _instance;
        public static Store Instance => _instance ??= new Store();

        // TODO: remove default, fill on auth
        public User? CurUser = Repositories.Instance.Users.FindByAuthData(email: "doctor1@example.com", password: "password");
    }
}
