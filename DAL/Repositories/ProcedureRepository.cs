using DAL.Entities;

namespace DAL.Repositories
{
    public class ProcedureRepository
    {
        private ClinicContext Context;

        public ProcedureRepository(ClinicContext context)
        {
            Context = context;
        }

        public void Create(Procedure data)
        {
            Context.Procedures.Add(data);
        }
    }
}
