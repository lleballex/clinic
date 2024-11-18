using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
{
    public class ClinicContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentResult> AppointmentResults { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Diagnosis> Diagnosises { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public DbSet<DoctorWorkDay> DoctorWorkDays { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: move to config
            optionsBuilder.UseNpgsql("Host=192.168.1.50;Port=5432;Username=leshalebedev;Database=clinic_new");
        }
    }
}
