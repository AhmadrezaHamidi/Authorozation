using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProducts",
                schema: "Base",
                table: "MyProducts");

            migrationBuilder.RenameTable(
                name: "MyProducts",
                schema: "Base",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.EnsureSchema(
                name: "Base");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "MyProducts",
                newSchema: "Base");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "Base",
                table: "MyProducts",
                newName: "ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                schema: "Base",
                table: "MyProducts",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProducts",
                schema: "Base",
                table: "MyProducts",
                column: "Id");
        }
    }
}
