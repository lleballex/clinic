using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "diagnosis",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnosis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doctor_specialization",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_specialization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    born_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "text", nullable: false),
                    street_id = table.Column<int>(type: "integer", nullable: false),
                    department_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Houses_Streets_street_id",
                        column: x => x.street_id,
                        principalTable: "Streets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Houses_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctor_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    appointment_duration = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    specialization_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_doctor_profile_doctor_specialization_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "doctor_specialization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_doctor_profile_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    medical_policy_number = table.Column<string>(type: "text", nullable: false),
                    born_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    house_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_Houses_house_id",
                        column: x => x.house_id,
                        principalTable: "Houses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentDoctorProfile",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "integer", nullable: false),
                    DoctorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDoctorProfile", x => new { x.DepartmentsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_DepartmentDoctorProfile_department_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentDoctorProfile_doctor_profile_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "doctor_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctor_work_day",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    week_day = table.Column<int>(type: "integer", nullable: false),
                    started_at = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ended_at = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_work_day", x => x.id);
                    table.ForeignKey(
                        name: "FK_doctor_work_day_doctor_profile_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "doctor_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointment_doctor_profile_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "doctor_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointment_result",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    diagnosis_description = table.Column<string>(type: "text", nullable: true),
                    recommendations = table.Column<string>(type: "text", nullable: true),
                    appointment_id = table.Column<int>(type: "integer", nullable: false),
                    diagnosis_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointment_result_appointment_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_result_diagnosis_diagnosis_id",
                        column: x => x.diagnosis_id,
                        principalTable: "diagnosis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_doctor_id",
                table: "appointment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_patient_id",
                table: "appointment",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_result_appointment_id",
                table: "appointment_result",
                column: "appointment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointment_result_diagnosis_id",
                table: "appointment_result",
                column: "diagnosis_id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDoctorProfile_DoctorsId",
                table: "DepartmentDoctorProfile",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_profile_specialization_id",
                table: "doctor_profile",
                column: "specialization_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_profile_user_id",
                table: "doctor_profile",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_work_day_doctor_id",
                table: "doctor_work_day",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_department_id",
                table: "Houses",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_street_id",
                table: "Houses",
                column: "street_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_house_id",
                table: "patient",
                column: "house_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment_result");

            migrationBuilder.DropTable(
                name: "DepartmentDoctorProfile");

            migrationBuilder.DropTable(
                name: "doctor_work_day");

            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "diagnosis");

            migrationBuilder.DropTable(
                name: "doctor_profile");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "doctor_specialization");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
