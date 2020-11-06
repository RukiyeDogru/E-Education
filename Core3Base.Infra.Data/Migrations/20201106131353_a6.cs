using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3Base.Infra.Data.Migrations
{
    public partial class a6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    LoginFailed = table.Column<int>(nullable: false),
                    LastLoginDate = table.Column<DateTime>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Explanation = table.Column<string>(nullable: true),
                    MailPermission = table.Column<bool>(nullable: false),
                    MailConfirmation = table.Column<bool>(nullable: false),
                    MailConfirmationCode = table.Column<string>(nullable: true),
                    IsChangePassword = table.Column<bool>(nullable: false),
                    ChangePasswordCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
