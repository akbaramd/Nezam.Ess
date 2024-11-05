using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nezam.Modular.ESS.Identity.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineers_Users_UserId",
                table: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_Engineers_UserId",
                table: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_UserId",
                table: "Employers");

            migrationBuilder.CreateIndex(
                name: "IX_Engineers_UserId",
                table: "Engineers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineers_Users_UserId",
                table: "Engineers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineers_Users_UserId",
                table: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_Engineers_UserId",
                table: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_UserId",
                table: "Employers");

            migrationBuilder.CreateIndex(
                name: "IX_Engineers_UserId",
                table: "Engineers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineers_Users_UserId",
                table: "Engineers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
