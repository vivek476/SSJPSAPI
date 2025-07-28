using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSJPSAPI.Migrations
{
    /// <inheritdoc />
    public partial class Appliedjobcomit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppliedJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedJobs_Employeejpes_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employeejpes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedJobs_Postjobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Postjobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJobs_EmployeeId",
                table: "AppliedJobs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJobs_JobId",
                table: "AppliedJobs",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedJobs");
        }
    }
}
