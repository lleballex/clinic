﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ClinicContext))]
    partial class ClinicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("datetime");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer")
                        .HasColumnName("doctor_id");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("appointment");
                });

            modelBuilder.Entity("DAL.Entities.AppointmentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("integer")
                        .HasColumnName("appointment_id");

                    b.Property<string>("DiagnosisDescription")
                        .HasColumnType("text")
                        .HasColumnName("diagnosis_description");

                    b.Property<int>("DiagnosisId")
                        .HasColumnType("integer")
                        .HasColumnName("diagnosis_id");

                    b.Property<string>("Recommendations")
                        .HasColumnType("text")
                        .HasColumnName("recommendations");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.HasIndex("DiagnosisId");

                    b.ToTable("appointment_result");
                });

            modelBuilder.Entity("DAL.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.ToTable("department");
                });

            modelBuilder.Entity("DAL.Entities.Diagnosis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("diagnosis");
                });

            modelBuilder.Entity("DAL.Entities.DoctorProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("AppointmentDuration")
                        .HasColumnType("time without time zone")
                        .HasColumnName("appointment_duration");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("department_id");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("integer")
                        .HasColumnName("specialization_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("doctor_profile");
                });

            modelBuilder.Entity("DAL.Entities.DoctorSpecialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("doctor_specialization");
                });

            modelBuilder.Entity("DAL.Entities.DoctorWorkDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer")
                        .HasColumnName("doctor_id");

                    b.Property<TimeOnly>("EndedAt")
                        .HasColumnType("time without time zone")
                        .HasColumnName("ended_at");

                    b.Property<TimeOnly>("StartedAt")
                        .HasColumnType("time without time zone")
                        .HasColumnName("started_at");

                    b.Property<int>("WeekDay")
                        .HasColumnType("integer")
                        .HasColumnName("week_day");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("doctor_work_day");
                });

            modelBuilder.Entity("DAL.Entities.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("department_id");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<int>("StreetId")
                        .HasColumnType("integer")
                        .HasColumnName("street_id");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("StreetId");

                    b.ToTable("house");
                });

            modelBuilder.Entity("DAL.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BornAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("born_at");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<int>("HouseId")
                        .HasColumnType("integer")
                        .HasColumnName("house_id");

                    b.Property<string>("MedicalPolicyNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("medical_policy_number");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("patient");
                });

            modelBuilder.Entity("DAL.Entities.Procedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("integer")
                        .HasColumnName("appointment_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("PatientId");

                    b.HasIndex("TypeId");

                    b.ToTable("procedure");
                });

            modelBuilder.Entity("DAL.Entities.ProcedureResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ProcedureId")
                        .HasColumnType("integer")
                        .HasColumnName("procedure_id");

                    b.Property<int>("ProcedurerId")
                        .HasColumnType("integer")
                        .HasColumnName("procedurer_id");

                    b.HasKey("Id");

                    b.HasIndex("ProcedureId")
                        .IsUnique();

                    b.HasIndex("ProcedurerId");

                    b.ToTable("procedure_result");
                });

            modelBuilder.Entity("DAL.Entities.ProcedureType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("procedure_type");
                });

            modelBuilder.Entity("DAL.Entities.ProcedurerProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("procedurer_profile");
                });

            modelBuilder.Entity("DAL.Entities.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("street");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BornAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("born_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ProcedureTypeProcedurerProfile", b =>
                {
                    b.Property<int>("ProcedurersId")
                        .HasColumnType("integer");

                    b.Property<int>("procedureTypesId")
                        .HasColumnType("integer");

                    b.HasKey("ProcedurersId", "procedureTypesId");

                    b.HasIndex("procedureTypesId");

                    b.ToTable("ProcedureTypeProcedurerProfile");
                });

            modelBuilder.Entity("DAL.Entities.Appointment", b =>
                {
                    b.HasOne("DAL.Entities.DoctorProfile", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DAL.Entities.AppointmentResult", b =>
                {
                    b.HasOne("DAL.Entities.Appointment", "Appointment")
                        .WithOne("Result")
                        .HasForeignKey("DAL.Entities.AppointmentResult", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Diagnosis", "Diagnosis")
                        .WithMany()
                        .HasForeignKey("DiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Diagnosis");
                });

            modelBuilder.Entity("DAL.Entities.DoctorProfile", b =>
                {
                    b.HasOne("DAL.Entities.Department", "Department")
                        .WithMany("Doctors")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("DAL.Entities.DoctorSpecialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("DAL.Entities.DoctorProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Specialization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.DoctorWorkDay", b =>
                {
                    b.HasOne("DAL.Entities.DoctorProfile", "Doctor")
                        .WithMany("WorkDays")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DAL.Entities.House", b =>
                {
                    b.HasOne("DAL.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Street", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Street");
                });

            modelBuilder.Entity("DAL.Entities.Patient", b =>
                {
                    b.HasOne("DAL.Entities.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("DAL.Entities.Procedure", b =>
                {
                    b.HasOne("DAL.Entities.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId");

                    b.HasOne("DAL.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.ProcedureType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Patient");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DAL.Entities.ProcedureResult", b =>
                {
                    b.HasOne("DAL.Entities.Procedure", "Procedure")
                        .WithOne("Result")
                        .HasForeignKey("DAL.Entities.ProcedureResult", "ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.ProcedurerProfile", "Procedurer")
                        .WithMany()
                        .HasForeignKey("ProcedurerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Procedure");

                    b.Navigation("Procedurer");
                });

            modelBuilder.Entity("DAL.Entities.ProcedurerProfile", b =>
                {
                    b.HasOne("DAL.Entities.User", "User")
                        .WithOne("Procedurer")
                        .HasForeignKey("DAL.Entities.ProcedurerProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProcedureTypeProcedurerProfile", b =>
                {
                    b.HasOne("DAL.Entities.ProcedurerProfile", null)
                        .WithMany()
                        .HasForeignKey("ProcedurersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.ProcedureType", null)
                        .WithMany()
                        .HasForeignKey("procedureTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.Appointment", b =>
                {
                    b.Navigation("Result");
                });

            modelBuilder.Entity("DAL.Entities.Department", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("DAL.Entities.DoctorProfile", b =>
                {
                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("DAL.Entities.Procedure", b =>
                {
                    b.Navigation("Result");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Procedurer");
                });
#pragma warning restore 612, 618
        }
    }
}
