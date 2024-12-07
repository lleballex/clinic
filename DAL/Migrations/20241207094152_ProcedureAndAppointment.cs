using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureAndAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_procedure_appointment_appointment_id",
                table: "procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_procedure_patient_patient_id",
                table: "procedure");

            migrationBuilder.DropTable(
                name: "procedure_result");

            migrationBuilder.DropTable(
                name: "ProcedureTypeProcedurerProfile");

            migrationBuilder.DropTable(
                name: "procedurer_profile");

            migrationBuilder.DropIndex(
                name: "IX_procedure_appointment_id",
                table: "procedure");

            migrationBuilder.DropIndex(
                name: "IX_procedure_patient_id",
                table: "procedure");

            migrationBuilder.DropColumn(
                name: "appointment_id",
                table: "procedure");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "procedure");

            migrationBuilder.DropColumn(
                name: "patient_id",
                table: "procedure");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "procedure",
                newName: "assigned_appointment_id");

            migrationBuilder.AddColumn<int>(
                name: "procedure_id",
                table: "appointment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_procedure_assigned_appointment_id",
                table: "procedure",
                column: "assigned_appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_procedure_id",
                table: "appointment",
                column: "procedure_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_procedure_procedure_id",
                table: "appointment",
                column: "procedure_id",
                principalTable: "procedure",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_procedure_appointment_assigned_appointment_id",
                table: "procedure",
                column: "assigned_appointment_id",
                principalTable: "appointment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_procedure_procedure_id",
                table: "appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_procedure_appointment_assigned_appointment_id",
                table: "procedure");

            migrationBuilder.DropIndex(
                name: "IX_procedure_assigned_appointment_id",
                table: "procedure");

            migrationBuilder.DropIndex(
                name: "IX_appointment_procedure_id",
                table: "appointment");

            migrationBuilder.DropColumn(
                name: "procedure_id",
                table: "appointment");

            migrationBuilder.RenameColumn(
                name: "assigned_appointment_id",
                table: "procedure",
                newName: "status");

            migrationBuilder.AddColumn<int>(
                name: "appointment_id",
                table: "procedure",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "procedure",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "patient_id",
                table: "procedure",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "procedurer_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedurer_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_procedurer_profile_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "procedure_result",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    procedure_id = table.Column<int>(type: "integer", nullable: false),
                    procedurer_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedure_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_procedure_result_procedure_procedure_id",
                        column: x => x.procedure_id,
                        principalTable: "procedure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_procedure_result_procedurer_profile_procedurer_id",
                        column: x => x.procedurer_id,
                        principalTable: "procedurer_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureTypeProcedurerProfile",
                columns: table => new
                {
                    ProcedurersId = table.Column<int>(type: "integer", nullable: false),
                    procedureTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureTypeProcedurerProfile", x => new { x.ProcedurersId, x.procedureTypesId });
                    table.ForeignKey(
                        name: "FK_ProcedureTypeProcedurerProfile_procedure_type_procedureType~",
                        column: x => x.procedureTypesId,
                        principalTable: "procedure_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureTypeProcedurerProfile_procedurer_profile_Procedure~",
                        column: x => x.ProcedurersId,
                        principalTable: "procedurer_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_procedure_appointment_id",
                table: "procedure",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedure_patient_id",
                table: "procedure",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedure_result_procedure_id",
                table: "procedure_result",
                column: "procedure_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_procedure_result_procedurer_id",
                table: "procedure_result",
                column: "procedurer_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedurer_profile_user_id",
                table: "procedurer_profile",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureTypeProcedurerProfile_procedureTypesId",
                table: "ProcedureTypeProcedurerProfile",
                column: "procedureTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_procedure_appointment_appointment_id",
                table: "procedure",
                column: "appointment_id",
                principalTable: "appointment",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_procedure_patient_patient_id",
                table: "procedure",
                column: "patient_id",
                principalTable: "patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
