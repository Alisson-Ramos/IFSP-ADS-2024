// PetGram — Rede social de animais
// Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
// Prof. Wellington Tuler Moraes
// Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

using Microsoft.EntityFrameworkCore;
using PetGram.Api.Data;
using PetGram.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Banco local SQLite (arquivo petgram.db criado na pasta do projeto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PetGram")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "PetGram API",
        Version = "v1",
        Description = "Rede social de animais — TP04 CBTSWE2 (IFSP Cubatão). " +
                      "Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares."
    });
});

var app = builder.Build();

// Cria o banco (se não existir) e insere dados de exemplo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Popular(db);
}

app.UseMiddleware<TratamentoExcecoesMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Página web (wwwroot/index.html)
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

// Permite referenciar o Program em testes, se necessário
public partial class Program { }
