// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using System.Text.RegularExpressions;

namespace PetGram.Core;

/// <summary>Extrai as hashtags (#exemplo) de uma legenda, como no Instagram.</summary>
public static class HashtagExtractor
{
    private static readonly Regex Padrao = new(@"#([\p{L}\p{N}_]+)", RegexOptions.Compiled);

    /// <summary>
    /// Retorna as hashtags da legenda, sem o "#", em minúsculas e sem repetição,
    /// na ordem em que aparecem.
    /// </summary>
    public static IReadOnlyList<string> Extrair(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return Array.Empty<string>();

        return Padrao.Matches(texto)
            .Select(m => m.Groups[1].Value.ToLowerInvariant())
            .Distinct()
            .ToList();
    }
}
