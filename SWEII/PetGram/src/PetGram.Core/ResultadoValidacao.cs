// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

namespace PetGram.Core;

/// <summary>Resultado de uma validação: lista de erros encontrados.</summary>
public sealed class ResultadoValidacao
{
    private readonly List<string> _erros = new();

    public IReadOnlyList<string> Erros => _erros;

    public bool Valido => _erros.Count == 0;

    internal void Adicionar(string erro) => _erros.Add(erro);
}
