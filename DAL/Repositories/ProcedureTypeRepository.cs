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

        public List<ProcedureType> FindAll()
        {
            return Context.ProcedureTypes.ToList();
        }
    }
}
