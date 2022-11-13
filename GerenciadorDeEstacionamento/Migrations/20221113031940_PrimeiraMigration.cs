using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeEstacionamento.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proprietario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaEstacionado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorDiaria = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeVagasDisponiveis = table.Column<int>(type: "int", nullable: false),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalFaturado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarroId = table.Column<int>(type: "int", nullable: false),
                    HorarioEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagas_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vagas_Patios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "Patios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_CarroId",
                table: "Vagas",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_PatioId",
                table: "Vagas",
                column: "PatioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Patios");
        }
    }
}
