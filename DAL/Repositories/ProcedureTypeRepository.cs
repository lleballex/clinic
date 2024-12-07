using DAL.Entities;

namespace DAL.Repositories
{
    public class ProcedureTypeRepository
    {
        private ClinicContext Context { get; set; }

        public ProcedureTypeRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<ProcedureType> FindAll(string? query = null)
        {
            return Context.ProcedureTypes
                .OrderBy(i => i.Name)
                .Where(i => (query == null || i.Name.Contains(query)))
                .ToList();
        }

        public void Create(ProcedureType data)
        {
            Context.ProcedureTypes.Add(data);
        }

        public void Update(ProcedureType data)
        {
            Context.ChangeTracker.Clear();
            Context.ProcedureTypes.Update(data);
        }

        public void Delete(ProcedureType data)
        {
            Context.ProcedureTypes.Remove(data);
        }
    }
}
