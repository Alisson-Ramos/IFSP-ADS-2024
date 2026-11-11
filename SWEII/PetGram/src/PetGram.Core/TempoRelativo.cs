// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using System.Globalization;

namespace PetGram.Core;

/// <summary>Formata datas no estilo do feed do Instagram: "agora", "há 5 min", "há 2 h", "há 3 d".</summary>
public static class TempoRelativo
{
    public static string Formatar(DateTime criadoEm, DateTime agora)
    {
        var diferenca = agora - criadoEm;

        if (diferenca < TimeSpan.FromMinutes(1)) return "agora";
        if (diferenca < TimeSpan.FromHours(1)) return $"há {(int)diferenca.TotalMinutes} min";
        if (diferenca < TimeSpan.FromDays(1)) return $"há {(int)diferenca.TotalHours} h";
        if (diferenca < TimeSpan.FromDays(7)) return $"há {(int)diferenca.TotalDays} d";

        return criadoEm.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
