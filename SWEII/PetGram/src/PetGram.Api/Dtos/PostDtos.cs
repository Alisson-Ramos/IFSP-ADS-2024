// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using PetGram.Api.Models;
using PetGram.Core;

namespace PetGram.Api.Dtos;

/// <summary>Dados enviados pela página para criar ou editar um post.</summary>
public record PostInput(int PetId, string? ImagemUrl, string? Legenda);

/// <summary>Post devolvido pela API, já com hashtags e tempo relativo calculados pela DLL PetGram.Core.</summary>
public record PostDto(
    int Id,
    int PetId,
    string PetNome,
    string PetUsuario,
    string PetEspecie,
    string? PetFotoUrl,
    string? ImagemUrl,
    string? Legenda,
    IReadOnlyList<string> Hashtags,
    int Curtidas,
    DateTime CriadoEm,
    string Tempo)
{
    public static PostDto De(Post post, DateTime agora) => new(
        post.Id,
        post.PetId,
        post.Pet?.Nome ?? string.Empty,
        post.Pet?.Usuario ?? string.Empty,
        post.Pet?.Especie ?? string.Empty,
        post.Pet?.FotoUrl,
        post.ImagemUrl,
        post.Legenda,
        HashtagExtractor.Extrair(post.Legenda),
        post.Curtidas,
        post.CriadoEm,
        TempoRelativo.Formatar(post.CriadoEm, agora));
}
