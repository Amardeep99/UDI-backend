using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDI_backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dnumber = table.Column<int>(type: "int", nullable: false),
                    traveldate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "references",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    applicationid = table.Column<int>(type: "int", nullable: false),
                    formid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_references", x => x.id);
                    table.ForeignKey(
                        name: "fk_references_applications_applicationid",
                        column: x => x.applicationid,
                        principalTable: "applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    referenceid = table.Column<int>(type: "int", nullable: false),
                    hasobjection = table.Column<bool>(type: "bit", nullable: false),
                    objectionreason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hasdebt = table.Column<bool>(type: "bit", nullable: false),
                    organisationnr = table.Column<int>(type: "int", nullable: false),
                    organisationname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_forms", x => x.id);
                    table.ForeignKey(
                        name: "fk_forms_references_referenceid",
                        column: x => x.referenceid,
                        principalTable: "references",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_forms_referenceid",
                table: "forms",
                column: "referenceid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_references_applicationid",
                table: "references",
                column: "applicationid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "forms");

            migrationBuilder.DropTable(
                name: "references");

            migrationBuilder.DropTable(
                name: "applications");
        }
    }
}
