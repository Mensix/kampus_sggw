using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusSggwBackend.Data.Migrations
{
    public partial class AddPasswordResetRequestsAndUserEmailChangesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResetRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvalidAttempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEmailChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailChanges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetRequests");

            migrationBuilder.DropTable(
                name: "UserEmailChanges");
        }
    }
}
