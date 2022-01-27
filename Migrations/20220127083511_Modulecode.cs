using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamEdu.Migrations
{
    public partial class Modulecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModuleCode",
                table: "Modules",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ModuleCode",
                table: "Modules",
                column: "ModuleCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Modules_ModuleCode",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ModuleCode",
                table: "Modules");
        }
    }
}
