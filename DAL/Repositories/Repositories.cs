using DAL.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.Repositories
{
    public class Repositories
    {
        private ClinicContext Context;

        public AppointmentRepository Appointments;
        public AppointmentResultRepository AppointmentsResults;
        public DepartmentRepository Departments;
        public DiagnosisRepository Diagnosises;
        public DoctorProfileRepository DoctorProfiles;
        public DoctorSpecializationRepository DoctorSpecializations;
        public HouseRepository Houses;
        public PatientRepository Patients;
        public StreetRepository Streets;
        public UserRepository Users;

        public Repositories()
        {
            Context = new ClinicContext();

            Appointments = new AppointmentRepository(Context);
            AppointmentsResults = new AppointmentResultRepository(Context);
            Departments = new DepartmentRepository(Context);
            Diagnosises = new DiagnosisRepository(Context);
            DoctorProfiles = new DoctorProfileRepository(Context);
            DoctorSpecializations = new DoctorSpecializationRepository(Context);
            Houses = new HouseRepository(Context);
            Patients = new PatientRepository(Context);
            Streets = new StreetRepository(Context);
            Users = new UserRepository(Context);
        }

        ~Repositories()
        {
            Context.Dispose();
        }
        
        private static Repositories? _instance;
        public static Repositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Repositories();
                }
                return _instance;
            }
        }

        public void UseTransaction(Action callback)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    callback();
                    transaction.Commit();
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
