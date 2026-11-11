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

/// <summary>CRUD dos perfis de pets.</summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PetsController : ControllerBase
{
    private readonly AppDbContext _db;

    public PetsController(AppDbContext db) => _db = db;

    /// <summary>Lista todos os pets.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetDto>>> Listar()
    {
        var pets = await _db.Pets
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .Select(p => new PetDto(p.Id, p.Nome, p.Usuario, p.Especie, p.Raca, p.Bio, p.FotoUrl, p.Posts.Count, p.CriadoEm))
            .ToListAsync();

        return Ok(pets);
    }

    /// <summary>Busca um pet pelo id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Obter(int id)
    {
        var pet = await _db.Pets
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PetDto(p.Id, p.Nome, p.Usuario, p.Especie, p.Raca, p.Bio, p.FotoUrl, p.Posts.Count, p.CriadoEm))
            .FirstOrDefaultAsync();

        return pet is null ? NotFound(Erro("Pet não encontrado.")) : Ok(pet);
    }

    /// <summary>Cria um pet.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PetDto>> Criar(PetInput input)
    {
        var validacao = PetValidator.Validar(input.Nome, input.Usuario, input.Especie, input.Bio, input.FotoUrl);
        if (!validacao.Valido) return BadRequest(new { erros = validacao.Erros });

        var usuario = PetValidator.NormalizarUsuario(input.Usuario);
        if (await _db.Pets.AnyAsync(p => p.Usuario == usuario))
            return Conflict(Erro($"O usuário @{usuario} já está em uso."));

        var pet = new Pet();
        Aplicar(pet, input, usuario);

        _db.Pets.Add(pet);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Obter), new { id = pet.Id }, ParaDto(pet, 0));
    }

    /// <summary>Atualiza um pet.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PetDto>> Atualizar(int id, PetInput input)
    {
        var pet = await _db.Pets.Include(p => p.Posts).FirstOrDefaultAsync(p => p.Id == id);
        if (pet is null) return NotFound(Erro("Pet não encontrado."));

        var validacao = PetValidator.Validar(input.Nome, input.Usuario, input.Especie, input.Bio, input.FotoUrl);
        if (!validacao.Valido) return BadRequest(new { erros = validacao.Erros });

        var usuario = PetValidator.NormalizarUsuario(input.Usuario);
        if (await _db.Pets.AnyAsync(p => p.Usuario == usuario && p.Id != id))
            return Conflict(Erro($"O usuário @{usuario} já está em uso."));

        Aplicar(pet, input, usuario);
        await _db.SaveChangesAsync();

        return Ok(ParaDto(pet, pet.Posts.Count));
    }

    /// <summary>Exclui um pet e todos os posts dele.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var pet = await _db.Pets.FindAsync(id);
        if (pet is null) return NotFound(Erro("Pet não encontrado."));

        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private static void Aplicar(Pet pet, PetInput input, string usuario)
    {
        pet.Nome = input.Nome.Trim();
        pet.Usuario = usuario;
        pet.Especie = input.Especie.Trim();
        pet.Raca = Limpar(input.Raca);
        pet.Bio = Limpar(input.Bio);
        pet.FotoUrl = Limpar(input.FotoUrl);
    }

    private static string? Limpar(string? valor) => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    private static PetDto ParaDto(Pet p, int totalPosts) =>
        new(p.Id, p.Nome, p.Usuario, p.Especie, p.Raca, p.Bio, p.FotoUrl, totalPosts, p.CriadoEm);

    private static object Erro(string mensagem) => new { erros = new[] { mensagem } };
}
