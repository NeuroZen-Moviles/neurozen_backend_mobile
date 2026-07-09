using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neurozen.API.Migrations
{
    /// <inheritdoc />
    public partial class UseProfessionalIdAsUserIdAndDropUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_users_UserId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "UX_Professionals_UserId",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Professionals");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_users_Id",
                table: "Professionals",
                column: "Id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_users_Id",
                table: "Professionals");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Professionals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "UX_Professionals_UserId",
                table: "Professionals",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_users_UserId",
                table: "Professionals",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
