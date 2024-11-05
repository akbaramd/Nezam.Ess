using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nezam.Modular.ESS.Identity.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVerificationToken",
                table: "UserVerificationToken");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserVerificationToken",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVerificationToken",
                table: "UserVerificationToken",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVerificationToken",
                table: "UserVerificationToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserVerificationToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVerificationToken",
                table: "UserVerificationToken",
                column: "Token");
        }
    }
}
