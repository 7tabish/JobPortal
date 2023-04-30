using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class addrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "jobid",
                table: "JobRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirements_jobid",
                table: "JobRequirements",
                column: "jobid");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequirements_Jobs_jobid",
                table: "JobRequirements",
                column: "jobid",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequirements_Jobs_jobid",
                table: "JobRequirements");

            migrationBuilder.DropIndex(
                name: "IX_JobRequirements_jobid",
                table: "JobRequirements");

            migrationBuilder.DropColumn(
                name: "jobid",
                table: "JobRequirements");
        }
    }
}
