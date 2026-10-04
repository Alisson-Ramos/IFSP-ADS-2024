// CBTSWE2 – TP03 – Sistema de Gerenciamento de Produtos
// Integrantes:
//   Alisson Ramos Aquino dos Santos
//   Nayara Pereira Soares

using CBTSWE2.TP03.Models;
using Microsoft.EntityFrameworkCore;

namespace CBTSWE2.TP03.Data
{
    // Camada de dados: o DbContext é a "ponte" entre os objetos C# e o SQL Server.
    // Ele é registrado na injeção de dependência (Program.cs) e recebido pelo controller.
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações complementares às DataAnnotations (Fluent API).
            modelBuilder.Entity<Produto>(entidade =>
            {
                entidade.HasIndex(p => p.Nome);
                entidade.Property(p => p.DataCadastro).HasDefaultValueSql("GETDATE()");
            });

            // Dados iniciais para a aplicação não começar vazia (entram pela migration).
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Notebook Dell Inspiron 15", Descricao = "Notebook com processador Intel Core i5, 8 GB de RAM e SSD de 256 GB.", Preco = 3499.90m, QuantidadeEstoque = 12, DataCadastro = new DateTime(2026, 9, 1, 9, 0, 0) },
                new Produto { Id = 2, Nome = "Mouse sem fio Logitech M170", Descricao = "Mouse óptico sem fio com receptor USB e alcance de até 10 metros.", Preco = 59.90m, QuantidadeEstoque = 80, DataCadastro = new DateTime(2026, 9, 2, 10, 30, 0) },
                new Produto { Id = 3, Nome = "Teclado mecânico Redragon Kumara", Descricao = "Teclado mecânico ABNT2 com switches outemu blue e iluminação LED vermelha.", Preco = 229.00m, QuantidadeEstoque = 5, DataCadastro = new DateTime(2026, 9, 3, 14, 15, 0) },
                new Produto { Id = 4, Nome = "Monitor LG 24 polegadas", Descricao = "Monitor IPS Full HD de 24 polegadas com entrada HDMI e DisplayPort.", Preco = 899.00m, QuantidadeEstoque = 0, DataCadastro = new DateTime(2026, 9, 4, 16, 45, 0) }
            );
        }
    }
}
