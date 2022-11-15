using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeEstacionamento.Migrations
{
    /// <inheritdoc />
    public partial class Patio1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Patios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Patios");
        }
    }
}
