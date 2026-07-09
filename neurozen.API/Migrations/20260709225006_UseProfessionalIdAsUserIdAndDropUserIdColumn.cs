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
            migrationBuilder.Sql(@"
SET @fk_exists := (
    SELECT COUNT(*)
    FROM information_schema.table_constraints
    WHERE table_schema = DATABASE()
      AND table_name = 'Professionals'
      AND constraint_name = 'FK_Professionals_users_UserId'
      AND constraint_type = 'FOREIGN KEY'
);
SET @sql := IF(
    @fk_exists > 0,
    'ALTER TABLE `Professionals` DROP FOREIGN KEY `FK_Professionals_users_UserId`',
    'SELECT 1'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;");

            migrationBuilder.Sql(@"
SET @idx_exists := (
    SELECT COUNT(*)
    FROM information_schema.statistics
    WHERE table_schema = DATABASE()
      AND table_name = 'Professionals'
      AND index_name = 'UX_Professionals_UserId'
);
SET @sql := IF(
    @idx_exists > 0,
    'DROP INDEX `UX_Professionals_UserId` ON `Professionals`',
    'SELECT 1'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;");

            migrationBuilder.Sql(@"
SET @column_exists := (
    SELECT COUNT(*)
    FROM information_schema.columns
    WHERE table_schema = DATABASE()
      AND table_name = 'Professionals'
      AND column_name = 'UserId'
);
SET @sql := IF(
    @column_exists > 0,
    'ALTER TABLE `Professionals` DROP COLUMN `UserId`',
    'SELECT 1'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;");

            migrationBuilder.Sql(@"
SET @new_fk_exists := (
    SELECT COUNT(*)
    FROM information_schema.table_constraints
    WHERE table_schema = DATABASE()
      AND table_name = 'Professionals'
      AND constraint_name = 'FK_Professionals_users_Id'
      AND constraint_type = 'FOREIGN KEY'
);
SET @sql := IF(
    @new_fk_exists = 0,
    'ALTER TABLE `Professionals` ADD CONSTRAINT `FK_Professionals_users_Id` FOREIGN KEY (`Id`) REFERENCES `users` (`Id`) ON DELETE CASCADE',
    'SELECT 1'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;");
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
