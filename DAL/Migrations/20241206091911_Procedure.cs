using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "procedure_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    ProcedurerProfileId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedure_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_procedure_type_procedurer_profile_ProcedurerProfileId",
                        column: x => x.ProcedurerProfileId,
                        principalTable: "procedurer_profile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "procedure",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    appointment_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedure", x => x.id);
                    table.ForeignKey(
                        name: "FK_procedure_appointment_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_procedure_patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_procedure_procedure_type_type_id",
                        column: x => x.type_id,
                        principalTable: "procedure_type",
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

            migrationBuilder.CreateIndex(
                name: "IX_procedure_appointment_id",
                table: "procedure",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedure_patient_id",
                table: "procedure",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedure_type_id",
                table: "procedure",
                column: "type_id");

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
                name: "IX_procedure_type_ProcedurerProfileId",
                table: "procedure_type",
                column: "ProcedurerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_procedurer_profile_user_id",
                table: "procedurer_profile",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "procedure_result");

            migrationBuilder.DropTable(
                name: "procedure");

            migrationBuilder.DropTable(
                name: "procedure_type");

            migrationBuilder.DropTable(
                name: "procedurer_profile");
        }
    }
}
