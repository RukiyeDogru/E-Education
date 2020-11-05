using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3Base.Infra.Data.Migrations
{
    public partial class a3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionExam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ExamId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionExam_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionExam_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionExam_ExamId",
                table: "QuestionExam",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionExam_QuestionId",
                table: "QuestionExam",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionExam");
        }
    }
}
