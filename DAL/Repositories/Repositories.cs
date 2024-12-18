﻿namespace DAL.Repositories
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
        public DoctorWorkDayRepository DoctorWorkDays;
        public HouseRepository Houses;
        public PatientRepository Patients;
        public ProcedureRepository Procedures;
        public ProcedureTypeRepository ProcedureTypes;
        public ReportRepository Reports;
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
            DoctorWorkDays = new DoctorWorkDayRepository(Context);
            Houses = new HouseRepository(Context);
            Patients = new PatientRepository(Context);
            Procedures = new ProcedureRepository(Context);
            ProcedureTypes = new ProcedureTypeRepository(Context);
            Reports = new ReportRepository(Context);
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
