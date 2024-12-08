using DAL.Entities;
using DAL.Repositories;

namespace Clinic
{
    public class Store
    {
        private static Store? _instance;
        public static Store Instance => _instance ??= new Store();

        public User? CurUser { get; set; }
    }
}
