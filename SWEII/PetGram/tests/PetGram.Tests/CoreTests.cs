// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using PetGram.Core;

namespace PetGram.Tests;

/// <summary>Testes da DLL PetGram.Core.</summary>
public class CoreTests
{
    // ---------- HashtagExtractor

    [Fact]
    public void Extrair_DevolveHashtagsEmMinusculasSemRepetir()
    {
        var tags = HashtagExtractor.Extrair("Dia de #Praia com o #dog e mais #praia!");

        Assert.Equal(new[] { "praia", "dog" }, tags);
    }

    [Fact]
    public void Extrair_AceitaAcentos()
    {
        var tags = HashtagExtractor.Extrair("Hoje é #preguiça");

        Assert.Equal(new[] { "preguiça" }, tags);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("sem hashtags aqui")]
    public void Extrair_SemHashtags_DevolveListaVazia(string? texto)
    {
        Assert.Empty(HashtagExtractor.Extrair(texto));
    }

    // ---------- PetValidator

    [Fact]
    public void ValidarPet_DadosCorretos_EhValido()
    {
        var r = PetValidator.Validar("Thor", "@Thor.Caramelo", "Cachorro", "Fã de bolinha", null);

        Assert.True(r.Valido);
    }

    [Theory]
    [InlineData("ab")]               // curto demais
    [InlineData("nome com espaço")]  // espaço
    [InlineData("thor!")]            // caractere inválido
    public void ValidarPet_UsuarioInvalido_RetornaErro(string usuario)
    {
        var r = PetValidator.Validar("Thor", usuario, "Cachorro", null, null);

        Assert.False(r.Valido);
        Assert.Contains(r.Erros, e => e.Contains("usuário", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void ValidarPet_CamposObrigatoriosVazios_RetornaTresErros()
    {
        var r = PetValidator.Validar("", "", "", null, null);

        Assert.Equal(3, r.Erros.Count);
    }

    [Fact]
    public void ValidarPet_FotoComUrlInvalida_RetornaErro()
    {
        var r = PetValidator.Validar("Mia", "mia_gata", "Gato", null, "ftp://foto.jpg");

        Assert.False(r.Valido);
    }

    [Fact]
    public void NormalizarUsuario_RemoveArrobaEspacosEMaiusculas()
    {
        Assert.Equal("mia_gata", PetValidator.NormalizarUsuario("  @Mia_Gata "));
    }

    // ---------- PostValidator

    [Fact]
    public void ValidarPost_SemImagemESemLegenda_RetornaErro()
    {
        var r = PostValidator.Validar(null, null);

        Assert.False(r.Valido);
    }

    [Fact]
    public void ValidarPost_LegendaMuitoLonga_RetornaErro()
    {
        var r = PostValidator.Validar(new string('a', PostValidator.LegendaMaxima + 1), null);

        Assert.False(r.Valido);
    }

    [Fact]
    public void ValidarPost_MaisDe30Hashtags_RetornaErro()
    {
        var legenda = string.Join(" ", Enumerable.Range(1, 31).Select(i => $"#tag{i}"));

        var r = PostValidator.Validar(legenda, null);

        Assert.False(r.Valido);
    }

    [Fact]
    public void ValidarPost_ImagemValida_EhValido()
    {
        var r = PostValidator.Validar(null, "https://exemplo.com/foto.jpg");

        Assert.True(r.Valido);
    }

    // ---------- TempoRelativo

    [Theory]
    [InlineData(30, "agora")]
    [InlineData(5 * 60, "há 5 min")]
    [InlineData(2 * 3600, "há 2 h")]
    [InlineData(3 * 86400, "há 3 d")]
    public void Formatar_DevolveTextoRelativo(int segundosAtras, string esperado)
    {
        var agora = new DateTime(2026, 11, 11, 12, 0, 0);

        Assert.Equal(esperado, TempoRelativo.Formatar(agora.AddSeconds(-segundosAtras), agora));
    }

    [Fact]
    public void Formatar_MaisDeUmaSemana_DevolveData()
    {
        var agora = new DateTime(2026, 11, 11);

        Assert.Equal("01/11/2026", TempoRelativo.Formatar(new DateTime(2026, 11, 1), agora));
    }
}
