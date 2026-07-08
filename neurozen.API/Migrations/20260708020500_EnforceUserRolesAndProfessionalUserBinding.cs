using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;

#nullable disable

namespace neurozen.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260708020500_EnforceUserRolesAndProfessionalUserBinding")]
public class EnforceUserRolesAndProfessionalUserBinding : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    // Normalize legacy role values before applying stricter constraints.
    migrationBuilder.Sql("UPDATE `users` SET `Role` = 'patient' WHERE `Role` IS NULL OR `Role` = '' OR `Role` = 'user';");

    migrationBuilder.AlterColumn<string>(
        name: "Role",
        table: "users",
        type: "varchar(20)",
        maxLength: 20,
        nullable: false,
        defaultValue: "patient",
        oldClrType: typeof(string),
        oldType: "varchar(50)",
        oldMaxLength: 50,
        oldDefaultValue: "user")
        .Annotation("MySql:CharSet", "utf8mb4")
        .OldAnnotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.AddCheckConstraint(
        name: "CK_users_role_allowed",
        table: "users",
        sql: "`Role` IN ('patient','professional')");

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

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropForeignKey(
        name: "FK_Professionals_users_UserId",
        table: "Professionals");

    migrationBuilder.DropIndex(
        name: "UX_Professionals_UserId",
        table: "Professionals");

    migrationBuilder.DropCheckConstraint(
        name: "CK_users_role_allowed",
        table: "users");

    migrationBuilder.AlterColumn<string>(
        name: "Role",
        table: "users",
        type: "varchar(50)",
        maxLength: 50,
        nullable: false,
        defaultValue: "user",
        oldClrType: typeof(string),
        oldType: "varchar(20)",
        oldMaxLength: 20,
        oldDefaultValue: "patient")
        .Annotation("MySql:CharSet", "utf8mb4")
        .OldAnnotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.Sql("UPDATE `users` SET `Role` = 'user' WHERE `Role` IS NULL OR `Role` = '';");
  }
}
