// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

namespace PetGram.Api.Models;

/// <summary>Perfil de um animal na rede social.</summary>
public class Pet
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string? Raca { get; set; }
    public string? Bio { get; set; }
    public string? FotoUrl { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public List<Post> Posts { get; set; } = new();
}
