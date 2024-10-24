using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDI_backend.Migrations
{
    /// <inheritdoc />
    public partial class refactor3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "objectionreason",
                table: "forms");

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline",
                table: "references",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(day, 14, GETUTCDATE())");

            migrationBuilder.AddColumn<DateTime>(
                name: "suggestedtraveldate",
                table: "forms",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deadline",
                table: "references");

            migrationBuilder.DropColumn(
                name: "suggestedtraveldate",
                table: "forms");

            migrationBuilder.AddColumn<string>(
                name: "objectionreason",
                table: "forms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
