using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ExamEdu.Migrations
{
    public partial class fix_examId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Modules_ExamId",
                table: "Exams");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Exams",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamName",
                table: "Exams",
                column: "ExamName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ModuleId",
                table: "Exams",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Modules_ModuleId",
                table: "Exams",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Modules_ModuleId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExamName",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ModuleId",
                table: "Exams");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Exams",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Modules_ExamId",
                table: "Exams",
                column: "ExamId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
