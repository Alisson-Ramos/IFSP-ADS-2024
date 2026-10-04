using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBTSWE2.TP03.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DataCadastro", "Descricao", "Nome", "Preco", "QuantidadeEstoque" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Notebook com processador Intel Core i5, 8 GB de RAM e SSD de 256 GB.", "Notebook Dell Inspiron 15", 3499.90m, 12 },
                    { 2, new DateTime(2026, 9, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Mouse óptico sem fio com receptor USB e alcance de até 10 metros.", "Mouse sem fio Logitech M170", 59.90m, 80 },
                    { 3, new DateTime(2026, 9, 3, 14, 15, 0, 0, DateTimeKind.Unspecified), "Teclado mecânico ABNT2 com switches outemu blue e iluminação LED vermelha.", "Teclado mecânico Redragon Kumara", 229.00m, 5 },
                    { 4, new DateTime(2026, 9, 4, 16, 45, 0, 0, DateTimeKind.Unspecified), "Monitor IPS Full HD de 24 polegadas com entrada HDMI e DisplayPort.", "Monitor LG 24 polegadas", 899.00m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
