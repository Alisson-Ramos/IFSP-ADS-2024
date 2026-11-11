// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetGram.Api.Middleware;

/// <summary>
/// Captura qualquer exceção não tratada e devolve um JSON padronizado (ProblemDetails),
/// em vez de deixar a API cair ou expor o stack trace.
/// </summary>
public class TratamentoExcecoesMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TratamentoExcecoesMiddleware> _logger;

    public TratamentoExcecoesMiddleware(RequestDelegate next, ILogger<TratamentoExcecoesMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro ao salvar no banco de dados.");
            await EscreverErroAsync(context, StatusCodes.Status409Conflict, "Não foi possível salvar: os dados conflitam com um registro existente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado.");
            await EscreverErroAsync(context, StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado no servidor.");
        }
    }

    private static async Task EscreverErroAsync(HttpContext context, int status, string titulo)
    {
        if (context.Response.HasStarted) return;

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails { Status = status, Title = titulo });
    }
}
