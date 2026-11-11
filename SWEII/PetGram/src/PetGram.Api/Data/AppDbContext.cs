// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using Microsoft.EntityFrameworkCore;
using PetGram.Api.Models;

namespace PetGram.Api.Data;

/// <summary>Contexto do Entity Framework Core (banco SQLite).</summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>(pet =>
        {
            pet.Property(p => p.Nome).HasMaxLength(40).IsRequired();
            pet.Property(p => p.Usuario).HasMaxLength(30).IsRequired();
            pet.Property(p => p.Especie).HasMaxLength(30).IsRequired();
            pet.Property(p => p.Raca).HasMaxLength(50);
            pet.Property(p => p.Bio).HasMaxLength(150);
            pet.HasIndex(p => p.Usuario).IsUnique();
        });

        modelBuilder.Entity<Post>(post =>
        {
            post.Property(p => p.Legenda).HasMaxLength(2200);
            post.HasOne(p => p.Pet)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
