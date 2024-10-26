using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDI_backend.Migrations
{
    /// <inheritdoc />
    public partial class referenceNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_forms_references_referenceid",
                table: "forms");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "references",
                newName: "referencenumber");

            migrationBuilder.RenameColumn(
                name: "referenceid",
                table: "forms",
                newName: "referencenumber");

            migrationBuilder.RenameIndex(
                name: "ix_forms_referenceid",
                table: "forms",
                newName: "ix_forms_referencenumber");

            migrationBuilder.AlterColumn<int>(
                name: "referencenumber",
                table: "references",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "fk_forms_references_referencenumber",
                table: "forms",
                column: "referencenumber",
                principalTable: "references",
                principalColumn: "referencenumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_forms_references_referencenumber",
                table: "forms");

            migrationBuilder.RenameColumn(
                name: "referencenumber",
                table: "references",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "referencenumber",
                table: "forms",
                newName: "referenceid");

            migrationBuilder.RenameIndex(
                name: "ix_forms_referencenumber",
                table: "forms",
                newName: "ix_forms_referenceid");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "references",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "fk_forms_references_referenceid",
                table: "forms",
                column: "referenceid",
                principalTable: "references",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
