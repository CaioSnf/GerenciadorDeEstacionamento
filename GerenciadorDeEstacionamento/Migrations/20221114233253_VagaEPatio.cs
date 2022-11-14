using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeEstacionamento.Migrations
{
    /// <inheritdoc />
    public partial class VagaEPatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Patios_PatioId",
                table: "Vagas");

            migrationBuilder.AlterColumn<int>(
                name: "PatioId",
                table: "Vagas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Patios_PatioId",
                table: "Vagas",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Patios_PatioId",
                table: "Vagas");

            migrationBuilder.AlterColumn<int>(
                name: "PatioId",
                table: "Vagas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Patios_PatioId",
                table: "Vagas",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id");
        }
    }
}
