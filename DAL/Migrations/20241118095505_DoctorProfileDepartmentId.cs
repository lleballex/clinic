using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class DoctorProfileDepartmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctor_profile_department_department_id",
                table: "doctor_profile");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "doctor_profile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_doctor_profile_department_department_id",
                table: "doctor_profile",
                column: "department_id",
                principalTable: "department",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctor_profile_department_department_id",
                table: "doctor_profile");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "doctor_profile",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_doctor_profile_department_department_id",
                table: "doctor_profile",
                column: "department_id",
                principalTable: "department",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
