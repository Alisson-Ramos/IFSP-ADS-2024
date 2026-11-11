// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

namespace PetGram.Api.Dtos;

/// <summary>Dados enviados pela página para criar ou editar um pet.</summary>
public record PetInput(string Nome, string Usuario, string Especie, string? Raca, string? Bio, string? FotoUrl);

/// <summary>Dados de um pet devolvidos pela API.</summary>
public record PetDto(
    int Id,
    string Nome,
    string Usuario,
    string Especie,
    string? Raca,
    string? Bio,
    string? FotoUrl,
    int TotalPosts,
    DateTime CriadoEm);
