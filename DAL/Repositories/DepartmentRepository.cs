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

        public List<Department> FindAll(string? query = null)
        {
            return Context.Departments
                .OrderBy(i => i.Number)
                .Where(i => (query == null || i.Number.ToString().Contains(query)))
                .ToList();
        }

        public void Create(Department data)
        {
            Context.Departments.Add(data);
        }

        public void Update(Department data)
        {
            Context.ChangeTracker.Clear();
            Context.Departments.Update(data);
        }

        public void Delete(Department data)
        {
            Context.Departments.Remove(data);
        }
    }
}
