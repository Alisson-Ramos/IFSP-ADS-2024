// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using PetGram.Api.Models;

namespace PetGram.Api.Data;

/// <summary>Dados iniciais para o banco não começar vazio na demonstração.</summary>
public static class SeedData
{
    public static void Popular(AppDbContext db)
    {
        if (db.Pets.Any()) return;

        var agora = DateTime.UtcNow;

        var thor = new Pet { Nome = "Thor", Usuario = "thor.caramelo", Especie = "Cachorro", Raca = "Vira-lata caramelo", Bio = "Patrimônio nacional 🇧🇷 | Fã de bolinha", CriadoEm = agora.AddDays(-30) };
        var mia = new Pet { Nome = "Mia", Usuario = "mia_gata", Especie = "Gato", Raca = "Siamês", Bio = "Dona da casa. Os humanos só moram aqui.", CriadoEm = agora.AddDays(-20) };
        var kiwi = new Pet { Nome = "Kiwi", Usuario = "kiwi.calopsita", Especie = "Pássaro", Raca = "Calopsita", Bio = "Canto às 6h da manhã, de nada.", CriadoEm = agora.AddDays(-10) };

        thor.Posts.Add(new Post { Legenda = "Primeiro passeio na praia de Santos! #praia #caramelo #dog", Curtidas = 42, CriadoEm = agora.AddHours(-3) });
        thor.Posts.Add(new Post { Legenda = "Banho tomado, agora ninguém me segura #cheiroso", Curtidas = 18, CriadoEm = agora.AddDays(-2) });
        mia.Posts.Add(new Post { Legenda = "Segunda-feira de novo? #gato #preguiça", Curtidas = 57, CriadoEm = agora.AddMinutes(-25) });
        kiwi.Posts.Add(new Post { Legenda = "Topete em dia ✨ #calopsita #bird", Curtidas = 23, CriadoEm = agora.AddDays(-1) });

        db.Pets.AddRange(thor, mia, kiwi);
        db.SaveChanges();
    }
}
