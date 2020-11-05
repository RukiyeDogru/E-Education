using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3Base.Infra.Data.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ClassName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    LessonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    Logged = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Logger = table.Column<string>(nullable: true),
                    Callsite = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    UserNo = table.Column<string>(nullable: true),
                    ActionTaken = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ShortContent = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    SubjectName = table.Column<string>(nullable: true),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Responce = table.Column<string>(nullable: true),
                    QuestionsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    LessonId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonStudents_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentQuestion_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Option = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    QuestionsId = table.Column<int>(nullable: true),
                    AnswerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AnswerId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    IsTrue = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionsId",
                table: "Answers",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonStudents_LessonId",
                table: "LessonStudents",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonStudents_StudentId",
                table: "LessonStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_AnswerId",
                table: "QuestionsAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_QuestionsId",
                table: "QuestionsAnswers",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_AnswerId",
                table: "StudentAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentId",
                table: "StudentAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestion_QuestionId",
                table: "StudentQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestion_StudentId",
                table: "StudentQuestion",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LessonId",
                table: "Students",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LessonId",
                table: "Subjects",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_LessonId",
                table: "Teachers",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "LessonStudents");

            migrationBuilder.DropTable(
                name: "LogDocuments");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers");

            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "StudentQuestion");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Classses");

            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
