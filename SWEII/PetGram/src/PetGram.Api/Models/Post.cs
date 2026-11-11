// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

namespace PetGram.Api.Models;

/// <summary>Publicação de um pet no feed.</summary>
public class Post
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public Pet? Pet { get; set; }
    public string? ImagemUrl { get; set; }
    public string? Legenda { get; set; }
    public int Curtidas { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
