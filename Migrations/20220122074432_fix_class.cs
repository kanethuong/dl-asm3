using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamEdu.Migrations
{
    public partial class fix_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "Classes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "Classes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
