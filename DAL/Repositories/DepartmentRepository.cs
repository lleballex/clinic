using DAL.Entities;

namespace DAL.Repositories
{
    public class DepartmentRepository
    {
        private ClinicContext Context;

        public DepartmentRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<Department> FindAll()
        {
            return Context.Departments.ToList();
        }
    }
}
