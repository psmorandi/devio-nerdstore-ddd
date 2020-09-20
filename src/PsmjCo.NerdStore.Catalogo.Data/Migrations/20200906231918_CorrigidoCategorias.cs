using Microsoft.EntityFrameworkCore.Migrations;

namespace PsmjCo.NerdStore.Catalogo.Data.Migrations
{
    public partial class CorrigidoCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Categorias");
        }
    }
}
