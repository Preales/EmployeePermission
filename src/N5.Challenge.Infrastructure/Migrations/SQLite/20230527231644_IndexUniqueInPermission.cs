﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N5.Challenge.Infrastructure.Migrations.SQLite
{
    /// <inheritdoc />
    public partial class IndexUniqueInPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_EmployeeId",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_EmployeeId_PermissionTypeId",
                table: "Permissions",
                columns: new[] { "EmployeeId", "PermissionTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_EmployeeId_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_EmployeeId",
                table: "Permissions",
                column: "EmployeeId");
        }
    }
}
