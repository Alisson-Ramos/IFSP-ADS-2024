// CBTSWE2 – TP03 – Sistema de Gerenciamento de Produtos
// Integrantes:
//   Alisson Ramos Aquino dos Santos
//   Nayara Pereira Soares

using System.Globalization;
using CBTSWE2.TP03.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework: registra o DbContext usando a connection string do appsettings.json.
builder.Services.AddDbContext<ProdutoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProdutoContext")
        ?? throw new InvalidOperationException("Connection string 'ProdutoContext' não encontrada.")));

// Mensagens padrão do model binding traduzidas (ex.: texto digitado no campo de quantidade).
builder.Services.AddControllersWithViews(options =>
{
    var mensagens = options.ModelBindingMessageProvider;
    mensagens.SetValueMustNotBeNullAccessor(_ => "Este campo é obrigatório.");
    mensagens.SetMissingBindRequiredValueAccessor(campo => $"O campo {campo} é obrigatório.");
    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"O valor '{valor}' não é válido para {campo}.");
    mensagens.SetUnknownValueIsInvalidAccessor(campo => $"Valor inválido para {campo}.");
    mensagens.SetValueIsInvalidAccessor(valor => $"O valor '{valor}' é inválido.");
    mensagens.SetValueMustBeANumberAccessor(campo => $"O campo {campo} deve ser um número.");
    mensagens.SetNonPropertyValueMustBeANumberAccessor(() => "Este campo deve ser um número.");
});

// URLs geradas em minúsculas (/produtos/editar/1 em vez de /Produtos/Editar/1).
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Cria/atualiza o banco aplicando as migrations pendentes.
// Assim basta rodar o projeto: não é preciso executar "Update-Database" manualmente.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProdutoContext>();
    context.Database.Migrate();
}

// Cultura pt-BR: preço com vírgula (19,90) e moeda em R$.
var culturaBrasil = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culturaBrasil),
    SupportedCultures = new[] { culturaBrasil },
    SupportedUICultures = new[] { culturaBrasil }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/erro");
    app.UseHsts();
}

// Página amigável para 404 (produto inexistente, URL errada etc.).
app.UseStatusCodePagesWithReExecute("/home/status/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota convencional {controller}/{action}/{id?}, usada pelo HomeController (/home/sobre).
// O ProdutosController usa rotas por atributo ([Route("produtos")], [HttpGet("editar/{id:int}")]...)
// e é ele quem responde pela página inicial "/".
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();
