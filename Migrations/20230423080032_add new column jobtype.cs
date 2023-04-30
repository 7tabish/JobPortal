using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumnjobtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryRange",
                table: "Jobs",
                newName: "JobType");

            migrationBuilder.AddColumn<float>(
                name: "Salary",
                table: "Jobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "creationDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "creationDate",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "Jobs",
                newName: "SalaryRange");
        }
    }
}
