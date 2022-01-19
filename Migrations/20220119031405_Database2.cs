using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamEdu.Migrations
{
    public partial class Database2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamMarks");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_ExamId_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Exam_FEQuestions_ExamId_FEQuestionId",
                table: "Exam_FEQuestions");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "AcademicDepartments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveAt",
                table: "Questions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveAt",
                table: "FEQuestions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "StudentExamInfos",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: true),
                    FinishAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamInfos", x => new { x.StudentId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_StudentExamInfos_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExamInfos_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamId_QuestionId_ExamCode",
                table: "ExamQuestions",
                columns: new[] { "ExamId", "QuestionId", "ExamCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_FEQuestions_ExamId_FEQuestionId_ExamCode",
                table: "Exam_FEQuestions",
                columns: new[] { "ExamId", "FEQuestionId", "ExamCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamInfos_ExamId",
                table: "StudentExamInfos",
                column: "ExamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExamInfos");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_ExamId_QuestionId_ExamCode",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Exam_FEQuestions_ExamId_FEQuestionId_ExamCode",
                table: "Exam_FEQuestions");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "Teachers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveAt",
                table: "Questions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveAt",
                table: "FEQuestions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "AcademicDepartments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ExamMarks",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    ExamId = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamMarks", x => new { x.StudentId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_ExamMarks_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamId_QuestionId",
                table: "ExamQuestions",
                columns: new[] { "ExamId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_FEQuestions_ExamId_FEQuestionId",
                table: "Exam_FEQuestions",
                columns: new[] { "ExamId", "FEQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_ExamId",
                table: "ExamMarks",
                column: "ExamId");
        }
    }
}
