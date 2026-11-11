// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using System.Text.RegularExpressions;

namespace PetGram.Core;

/// <summary>Regras de validação do perfil de um pet.</summary>
public static class PetValidator
{
    public const int NomeMaximo = 40;
    public const int BioMaxima = 150;

    // Mesmo formato de nome de usuário do Instagram: letras minúsculas, números, ponto e sublinhado.
    private static readonly Regex UsuarioPadrao = new(@"^[a-z0-9._]{3,30}$", RegexOptions.Compiled);

    /// <summary>Remove espaços e o "@" inicial e deixa o usuário em minúsculas.</summary>
    public static string NormalizarUsuario(string? usuario) =>
        (usuario ?? string.Empty).Trim().TrimStart('@').ToLowerInvariant();

    public static ResultadoValidacao Validar(string? nome, string? usuario, string? especie, string? bio, string? fotoUrl)
    {
        var resultado = new ResultadoValidacao();

        if (string.IsNullOrWhiteSpace(nome))
            resultado.Adicionar("O nome do pet é obrigatório.");
        else if (nome.Trim().Length > NomeMaximo)
            resultado.Adicionar($"O nome deve ter no máximo {NomeMaximo} caracteres.");

        var usuarioNormalizado = NormalizarUsuario(usuario);
        if (usuarioNormalizado.Length == 0)
            resultado.Adicionar("O nome de usuário é obrigatório.");
        else if (!UsuarioPadrao.IsMatch(usuarioNormalizado))
            resultado.Adicionar("O usuário deve ter de 3 a 30 caracteres: letras, números, ponto ou sublinhado.");

        if (string.IsNullOrWhiteSpace(especie))
            resultado.Adicionar("A espécie é obrigatória.");

        if (bio is not null && bio.Length > BioMaxima)
            resultado.Adicionar($"A bio deve ter no máximo {BioMaxima} caracteres.");

        if (!string.IsNullOrWhiteSpace(fotoUrl) && !Url.EhValida(fotoUrl))
            resultado.Adicionar("A URL da foto de perfil é inválida (use http ou https).");

        return resultado;
    }
}
