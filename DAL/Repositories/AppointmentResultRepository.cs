using DAL.Entities;

namespace DAL.Repositories
{
    public class AppointmentResultRepository
    {
        private ClinicContext Context { get; set; }

        public AppointmentResultRepository(ClinicContext context)
        {
            Context = context;
        }

        public void Create(AppointmentResult data)
        {
            Context.AppointmentResults.Add(data);
        }
    }
}
