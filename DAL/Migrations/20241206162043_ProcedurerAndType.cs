using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProcedurerAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_procedure_type_procedurer_profile_ProcedurerProfileId",
                table: "procedure_type");

            migrationBuilder.DropIndex(
                name: "IX_procedure_type_ProcedurerProfileId",
                table: "procedure_type");

            migrationBuilder.DropColumn(
                name: "ProcedurerProfileId",
                table: "procedure_type");

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
                name: "IX_ProcedureTypeProcedurerProfile_procedureTypesId",
                table: "ProcedureTypeProcedurerProfile",
                column: "procedureTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureTypeProcedurerProfile");

            migrationBuilder.AddColumn<int>(
                name: "ProcedurerProfileId",
                table: "procedure_type",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_procedure_type_ProcedurerProfileId",
                table: "procedure_type",
                column: "ProcedurerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_procedure_type_procedurer_profile_ProcedurerProfileId",
                table: "procedure_type",
                column: "ProcedurerProfileId",
                principalTable: "procedurer_profile",
                principalColumn: "id");
        }
    }
}
