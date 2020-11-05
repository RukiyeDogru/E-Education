using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3Base.Infra.Data.Migrations
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Lessons_LessonId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionExam_Exam_ExamId",
                table: "QuestionExam");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionExam_Questions_QuestionId",
                table: "QuestionExam");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentQuestion_Questions_QuestionId",
                table: "StudentQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentQuestion_Students_StudentId",
                table: "StudentQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentQuestion",
                table: "StudentQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionExam",
                table: "QuestionExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exam",
                table: "Exam");

            migrationBuilder.RenameTable(
                name: "StudentQuestion",
                newName: "StudentQuestions");

            migrationBuilder.RenameTable(
                name: "QuestionExam",
                newName: "QuestionExams");

            migrationBuilder.RenameTable(
                name: "Exam",
                newName: "Exams");

            migrationBuilder.RenameIndex(
                name: "IX_StudentQuestion_StudentId",
                table: "StudentQuestions",
                newName: "IX_StudentQuestions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentQuestion_QuestionId",
                table: "StudentQuestions",
                newName: "IX_StudentQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionExam_QuestionId",
                table: "QuestionExams",
                newName: "IX_QuestionExams_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionExam_ExamId",
                table: "QuestionExams",
                newName: "IX_QuestionExams_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Exam_LessonId",
                table: "Exams",
                newName: "IX_Exams_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentQuestions",
                table: "StudentQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionExams",
                table: "QuestionExams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exams",
                table: "Exams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Lessons_LessonId",
                table: "Exams",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionExams_Exams_ExamId",
                table: "QuestionExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionExams_Questions_QuestionId",
                table: "QuestionExams",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentQuestions_Questions_QuestionId",
                table: "StudentQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentQuestions_Students_StudentId",
                table: "StudentQuestions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Lessons_LessonId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionExams_Exams_ExamId",
                table: "QuestionExams");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionExams_Questions_QuestionId",
                table: "QuestionExams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentQuestions_Questions_QuestionId",
                table: "StudentQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentQuestions_Students_StudentId",
                table: "StudentQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentQuestions",
                table: "StudentQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionExams",
                table: "QuestionExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exams",
                table: "Exams");

            migrationBuilder.RenameTable(
                name: "StudentQuestions",
                newName: "StudentQuestion");

            migrationBuilder.RenameTable(
                name: "QuestionExams",
                newName: "QuestionExam");

            migrationBuilder.RenameTable(
                name: "Exams",
                newName: "Exam");

            migrationBuilder.RenameIndex(
                name: "IX_StudentQuestions_StudentId",
                table: "StudentQuestion",
                newName: "IX_StudentQuestion_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentQuestions_QuestionId",
                table: "StudentQuestion",
                newName: "IX_StudentQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionExams_QuestionId",
                table: "QuestionExam",
                newName: "IX_QuestionExam_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionExams_ExamId",
                table: "QuestionExam",
                newName: "IX_QuestionExam_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_LessonId",
                table: "Exam",
                newName: "IX_Exam_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentQuestion",
                table: "StudentQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionExam",
                table: "QuestionExam",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exam",
                table: "Exam",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Lessons_LessonId",
                table: "Exam",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionExam_Exam_ExamId",
                table: "QuestionExam",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionExam_Questions_QuestionId",
                table: "QuestionExam",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentQuestion_Questions_QuestionId",
                table: "StudentQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentQuestion_Students_StudentId",
                table: "StudentQuestion",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
