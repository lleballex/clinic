using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PatientRepository
    {
        private ClinicContext Context;

        public PatientRepository(ClinicContext context)
        {
            Context = context;
        }

        public List<Patient> FindAll(string query = "")
        {
            return Context.Patients
                .Include(i => i.House)
                .ThenInclude(i => i.Department)
                .Include(i => i.House)
                .ThenInclude(i => i.Street)
                .Where(i => (i.Surname + " " + i.Name + " " + i.Patronymic).Contains(query))
                .OrderBy(i => i.Surname)
                .ToList();
        }

        public void Create(Patient data)
        {
            Context.Patients.Add(data);
        }

        public void Update(Patient data)
        {
            Context.ChangeTracker.Clear();
            Context.Patients.Update(data);
        }

        public void Delete(Patient data)
        {
            Context.Patients.Remove(data);
        }
    }
}
