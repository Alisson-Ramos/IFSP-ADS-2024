// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

namespace PetGram.Core;

/// <summary>Regras de validação de uma publicação (post). Limites iguais aos do Instagram.</summary>
public static class PostValidator
{
    public const int LegendaMaxima = 2200;
    public const int HashtagsMaximas = 30;

    public static ResultadoValidacao Validar(string? legenda, string? imagemUrl)
    {
        var resultado = new ResultadoValidacao();

        if (string.IsNullOrWhiteSpace(legenda) && string.IsNullOrWhiteSpace(imagemUrl))
            resultado.Adicionar("O post precisa de uma imagem ou de uma legenda.");

        if (legenda is not null && legenda.Length > LegendaMaxima)
            resultado.Adicionar($"A legenda deve ter no máximo {LegendaMaxima} caracteres.");

        if (HashtagExtractor.Extrair(legenda).Count > HashtagsMaximas)
            resultado.Adicionar($"Use no máximo {HashtagsMaximas} hashtags por post.");

        if (!string.IsNullOrWhiteSpace(imagemUrl) && !Url.EhValida(imagemUrl))
            resultado.Adicionar("A URL da imagem é inválida (use http ou https).");

        return resultado;
    }
}
