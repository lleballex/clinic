using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System.Configuration;

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
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["DbConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Procedure>()
                .HasOne(i => i.AssignedAppointment)
                .WithMany(i => i.AssignedProcedures)
                .HasForeignKey(i => i.AssignedAppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Appointment>()
                .HasOne(i => i.Procedure)
                .WithOne(i => i.Appointment)
                .HasForeignKey<Appointment>(i => i.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
