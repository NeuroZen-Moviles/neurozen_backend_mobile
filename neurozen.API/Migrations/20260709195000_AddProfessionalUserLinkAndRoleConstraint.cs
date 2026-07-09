using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;

#nullable disable

namespace neurozen.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260709195000_AddProfessionalUserLinkAndRoleConstraint")]
public class AddProfessionalUserLinkAndRoleConstraint : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<string>(
        name: "Email",
        table: "Professionals",
        type: "varchar(255)",
        maxLength: 255,
        nullable: false,
        defaultValue: "")
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.AddColumn<Guid>(
        name: "UserId",
        table: "Professionals",
        type: "char(36)",
        nullable: true,
        collation: "ascii_general_ci");

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

    migrationBuilder.Sql(@"
INSERT INTO `users` (`Id`, `Username`, `Email`, `PasswordHash`, `FullName`, `Role`, `AvatarUrl`, `Meta`)
VALUES
('97b437da-1334-44b5-b8ce-414fc7470e90', 'dr-enzo-alatrista', 'dr-enzo-alatrista@neurozen.local', NULL, 'Dr. Enzo Alatrista', 'professional', NULL, '{}'),
('ef4f2cb4-8022-46da-b5a6-4462e3cd3e08', 'yunyin', 'yunyin@neurozen.local', NULL, 'Yunyin', 'professional', NULL, '{}')
ON DUPLICATE KEY UPDATE
  `Username` = VALUES(`Username`),
  `Email` = VALUES(`Email`),
  `FullName` = VALUES(`FullName`),
  `Role` = VALUES(`Role`),
  `Meta` = VALUES(`Meta`);");

    migrationBuilder.Sql("UPDATE `Professionals` SET `Email` = 'dr-enzo-alatrista@neurozen.local', `UserId` = '97b437da-1334-44b5-b8ce-414fc7470e90' WHERE `Id` = '97b437da-1334-44b5-b8ce-414fc7470e90';");
    migrationBuilder.Sql("UPDATE `Professionals` SET `Email` = 'yunyin@neurozen.local', `UserId` = 'ef4f2cb4-8022-46da-b5a6-4462e3cd3e08' WHERE `Id` = 'ef4f2cb4-8022-46da-b5a6-4462e3cd3e08';");

    migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Professionals",
        type: "char(36)",
        nullable: false,
        collation: "ascii_general_ci",
        oldClrType: typeof(Guid),
        oldType: "char(36)",
        oldNullable: true)
        .OldAnnotation("MySql:CharSet", "utf8mb4");

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

    migrationBuilder.Sql("DELETE FROM `users` WHERE `Id` IN ('97b437da-1334-44b5-b8ce-414fc7470e90', 'ef4f2cb4-8022-46da-b5a6-4462e3cd3e08');");

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

    migrationBuilder.Sql("UPDATE `users` SET `Role` = 'user' WHERE `Role` = 'patient';");

    migrationBuilder.DropColumn(
        name: "UserId",
        table: "Professionals");

    migrationBuilder.DropColumn(
        name: "Email",
        table: "Professionals");
  }
}