using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDI_backend.Migrations
{
    /// <inheritdoc />
    public partial class refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "organisationname",
                table: "forms");

            migrationBuilder.DropColumn(
                name: "organisationnr",
                table: "forms");

            migrationBuilder.AddColumn<int>(
                name: "organisationnr",
                table: "references",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "organisationnr",
                table: "references");

            migrationBuilder.AddColumn<string>(
                name: "organisationname",
                table: "forms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "organisationnr",
                table: "forms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
