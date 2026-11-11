// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetGram.Api.Controllers;
using PetGram.Api.Data;
using PetGram.Api.Dtos;
using PetGram.Api.Models;

namespace PetGram.Tests;

/// <summary>Testes dos controllers da Web API usando banco em memória.</summary>
public class ApiTests
{
    // Cada teste recebe um banco novo e isolado
    private static AppDbContext NovoBanco()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private static async Task<Pet> CriarPetAsync(AppDbContext db, string usuario = "thor.caramelo")
    {
        var pet = new Pet { Nome = "Thor", Usuario = usuario, Especie = "Cachorro" };
        db.Pets.Add(pet);
        await db.SaveChangesAsync();
        return pet;
    }

    // ---------- Pets

    [Fact]
    public async Task CriarPet_DadosValidos_Retorna201ESalvaUsuarioNormalizado()
    {
        using var db = NovoBanco();
        var controller = new PetsController(db);

        var resposta = await controller.Criar(new PetInput("Mia", "@Mia_Gata", "Gato", "Siamês", null, null));

        var criado = Assert.IsType<CreatedAtActionResult>(resposta.Result);
        var dto = Assert.IsType<PetDto>(criado.Value);
        Assert.Equal("mia_gata", dto.Usuario);
        Assert.Equal(1, await db.Pets.CountAsync());
    }

    [Fact]
    public async Task CriarPet_UsuarioRepetido_Retorna409()
    {
        using var db = NovoBanco();
        await CriarPetAsync(db, "thor.caramelo");
        var controller = new PetsController(db);

        var resposta = await controller.Criar(new PetInput("Outro Thor", "thor.caramelo", "Cachorro", null, null, null));

        Assert.IsType<ConflictObjectResult>(resposta.Result);
    }

    [Fact]
    public async Task CriarPet_DadosInvalidos_Retorna400()
    {
        using var db = NovoBanco();
        var controller = new PetsController(db);

        var resposta = await controller.Criar(new PetInput("", "x", "", null, null, null));

        Assert.IsType<BadRequestObjectResult>(resposta.Result);
        Assert.Equal(0, await db.Pets.CountAsync());
    }

    [Fact]
    public async Task ObterPet_Inexistente_Retorna404()
    {
        using var db = NovoBanco();
        var controller = new PetsController(db);

        var resposta = await controller.Obter(999);

        Assert.IsType<NotFoundObjectResult>(resposta.Result);
    }

    [Fact]
    public async Task AtualizarPet_AlteraOsDados()
    {
        using var db = NovoBanco();
        var pet = await CriarPetAsync(db);
        var controller = new PetsController(db);

        var resposta = await controller.Atualizar(pet.Id, new PetInput("Thor Jr.", "thor.jr", "Cachorro", "SRD", "Nova bio", null));

        var ok = Assert.IsType<OkObjectResult>(resposta.Result);
        var dto = Assert.IsType<PetDto>(ok.Value);
        Assert.Equal("Thor Jr.", dto.Nome);
        Assert.Equal("thor.jr", dto.Usuario);
    }

    [Fact]
    public async Task ExcluirPet_RemoveDoBanco()
    {
        using var db = NovoBanco();
        var pet = await CriarPetAsync(db);
        var controller = new PetsController(db);

        var resposta = await controller.Excluir(pet.Id);

        Assert.IsType<NoContentResult>(resposta);
        Assert.Equal(0, await db.Pets.CountAsync());
    }

    // ---------- Posts

    [Fact]
    public async Task CriarPost_Valido_Retorna201ComHashtags()
    {
        using var db = NovoBanco();
        var pet = await CriarPetAsync(db);
        var controller = new PostsController(db);

        var resposta = await controller.Criar(new PostInput(pet.Id, null, "Passeio na #praia com o #dog"));

        var criado = Assert.IsType<CreatedAtActionResult>(resposta.Result);
        var dto = Assert.IsType<PostDto>(criado.Value);
        Assert.Equal(new[] { "praia", "dog" }, dto.Hashtags);
        Assert.Equal("agora", dto.Tempo);
        Assert.Equal("thor.caramelo", dto.PetUsuario);
    }

    [Fact]
    public async Task CriarPost_PetInexistente_Retorna400()
    {
        using var db = NovoBanco();
        var controller = new PostsController(db);

        var resposta = await controller.Criar(new PostInput(123, null, "Olá"));

        Assert.IsType<BadRequestObjectResult>(resposta.Result);
    }

    [Fact]
    public async Task ListarPosts_FiltraPorHashtag()
    {
        using var db = NovoBanco();
        var pet = await CriarPetAsync(db);
        db.Posts.AddRange(
            new Post { PetId = pet.Id, Legenda = "Dia de #praia" },
            new Post { PetId = pet.Id, Legenda = "Dia de #banho" });
        await db.SaveChangesAsync();
        var controller = new PostsController(db);

        var resposta = await controller.Listar(null, "#Praia");

        var ok = Assert.IsType<OkObjectResult>(resposta.Result);
        var posts = Assert.IsAssignableFrom<IEnumerable<PostDto>>(ok.Value);
        Assert.Single(posts);
    }

    [Fact]
    public async Task CurtirEDescurtir_AlteraContadorSemFicarNegativo()
    {
        using var db = NovoBanco();
        var pet = await CriarPetAsync(db);
        var post = new Post { PetId = pet.Id, Legenda = "Oi", Curtidas = 0 };
        db.Posts.Add(post);
        await db.SaveChangesAsync();
        var controller = new PostsController(db);

        await controller.Curtir(post.Id);
        await controller.Descurtir(post.Id);
        await controller.Descurtir(post.Id); // não pode ficar -1

        Assert.Equal(0, (await db.Posts.FindAsync(post.Id))!.Curtidas);
    }

    [Fact]
    public async Task ExcluirPost_Inexistente_Retorna404()
    {
        using var db = NovoBanco();
        var controller = new PostsController(db);

        var resposta = await controller.Excluir(42);

        Assert.IsType<NotFoundObjectResult>(resposta);
    }
}
