// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetGram.Api.Data;
using PetGram.Api.Dtos;
using PetGram.Api.Models;
using PetGram.Core;

namespace PetGram.Api.Controllers;

/// <summary>CRUD das publicações (feed) e curtidas.</summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _db;

    public PostsController(AppDbContext db) => _db = db;

    /// <summary>Lista o feed, do mais recente para o mais antigo. Filtros opcionais: petId e hashtag.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> Listar([FromQuery] int? petId, [FromQuery] string? hashtag)
    {
        var consulta = _db.Posts.AsNoTracking().Include(p => p.Pet).AsQueryable();

        if (petId.HasValue)
            consulta = consulta.Where(p => p.PetId == petId.Value);

        var posts = await consulta.OrderByDescending(p => p.CriadoEm).ToListAsync();

        var agora = DateTime.UtcNow;
        var resultado = posts.Select(p => PostDto.De(p, agora));

        // O filtro por hashtag usa a DLL PetGram.Core, por isso é feito em memória.
        if (!string.IsNullOrWhiteSpace(hashtag))
        {
            var tag = hashtag.Trim().TrimStart('#').ToLowerInvariant();
            resultado = resultado.Where(p => p.Hashtags.Contains(tag));
        }

        return Ok(resultado.ToList());
    }

    /// <summary>Busca um post pelo id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> Obter(int id)
    {
        var post = await _db.Posts.AsNoTracking().Include(p => p.Pet).FirstOrDefaultAsync(p => p.Id == id);
        return post is null ? NotFound(Erro("Post não encontrado.")) : Ok(PostDto.De(post, DateTime.UtcNow));
    }

    /// <summary>Publica um post.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostDto>> Criar(PostInput input)
    {
        var validacao = PostValidator.Validar(input.Legenda, input.ImagemUrl);
        if (!validacao.Valido) return BadRequest(new { erros = validacao.Erros });

        var pet = await _db.Pets.FindAsync(input.PetId);
        if (pet is null) return BadRequest(Erro("Selecione um pet válido."));

        var post = new Post
        {
            PetId = pet.Id,
            Pet = pet,
            ImagemUrl = Limpar(input.ImagemUrl),
            Legenda = Limpar(input.Legenda),
            CriadoEm = DateTime.UtcNow
        };

        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Obter), new { id = post.Id }, PostDto.De(post, DateTime.UtcNow));
    }

    /// <summary>Edita a imagem e/ou a legenda de um post.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> Atualizar(int id, PostInput input)
    {
        var post = await _db.Posts.Include(p => p.Pet).FirstOrDefaultAsync(p => p.Id == id);
        if (post is null) return NotFound(Erro("Post não encontrado."));

        var validacao = PostValidator.Validar(input.Legenda, input.ImagemUrl);
        if (!validacao.Valido) return BadRequest(new { erros = validacao.Erros });

        post.ImagemUrl = Limpar(input.ImagemUrl);
        post.Legenda = Limpar(input.Legenda);
        await _db.SaveChangesAsync();

        return Ok(PostDto.De(post, DateTime.UtcNow));
    }

    /// <summary>Exclui um post.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return NotFound(Erro("Post não encontrado."));

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Curte um post (+1).</summary>
    [HttpPost("{id:int}/curtir")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Curtir(int id) => AlterarCurtidas(id, +1);

    /// <summary>Remove a curtida de um post (-1, nunca abaixo de zero).</summary>
    [HttpDelete("{id:int}/curtir")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Descurtir(int id) => AlterarCurtidas(id, -1);

    private async Task<IActionResult> AlterarCurtidas(int id, int delta)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return NotFound(Erro("Post não encontrado."));

        post.Curtidas = Math.Max(0, post.Curtidas + delta);
        await _db.SaveChangesAsync();

        return Ok(new { id = post.Id, curtidas = post.Curtidas });
    }

    private static string? Limpar(string? valor) => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    private static object Erro(string mensagem) => new { erros = new[] { mensagem } };
}
