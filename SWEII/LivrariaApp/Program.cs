using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text;
using LivrariaApp;
using System;

// 1. D) Gerar o "produto final", rodando os testes no console (Prompt)
TestClass.RunTests();

Console.WriteLine("\n=== Iniciando Aplicação Web (Item 2) ===");
Console.WriteLine("As rotas a seguir estarão disponíveis assim que o servidor iniciar (verifique a porta abaixo):");
Console.WriteLine("B1 - /livro/nome");
Console.WriteLine("B2 - /livro/toString");
Console.WriteLine("B3 - /livro/autores");
Console.WriteLine("B4 - /livro/ApresentarLivro");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Usando um livro de exemplo para as rotas
Author a1 = new Author("J.R.R. Tolkien", "tolkien@example.com", 'M');
Author a2 = new Author("Christopher Tolkien", "chris@example.com", 'M');
Book myBook = new Book("O Silmarillion", new Author[] { a1, a2 }, 59.90, 10);

// B1 – O nome do livro.
app.MapGet("/livro/nome", () => myBook.GetName());

// B2 – O resultado do método toString().
app.MapGet("/livro/toString", () => myBook.ToString());

// B3 – O resultado do método GetAuthorNames().
app.MapGet("/livro/autores", () => myBook.GetAuthorNames());

// B4 – Gerar uma página HTML que responda a Rota /livro/ApresentarLivro
app.MapGet("/livro/ApresentarLivro", () => 
{
    var html = $@"
    <!DOCTYPE html>
    <html lang='pt-BR'>
    <head>
        <meta charset='UTF-8'>
        <title>Apresentação do Livro</title>
        <style>
            body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f7f6; color: #333; margin: 40px; }}
            .card {{ background: #fff; padding: 30px; border-radius: 12px; box-shadow: 0 8px 16px rgba(0,0,0,0.1); max-width: 600px; margin: auto; transition: transform 0.3s; }}
            .card:hover {{ transform: translateY(-5px); }}
            h1 {{ color: #2c3e50; text-align: center; margin-bottom: 5px; }}
            p {{ font-size: 1.1em; line-height: 1.6; margin: 10px 0; }}
            .authors {{ color: #7f8c8d; font-style: italic; text-align: center; margin-top: 0; margin-bottom: 20px; }}
            .price {{ font-weight: bold; color: #27ae60; font-size: 1.4em; text-align: center; margin: 20px 0; }}
            .details {{ background: #fdfdfd; padding: 15px; border-radius: 8px; border: 1px solid #eee; }}
        </style>
    </head>
    <body>
        <div class='card'>
            <h1>{myBook.GetName()}</h1>
            <p class='authors'>Por {myBook.GetAuthorNames()}</p>
            
            <div class='details'>
                <p><strong>Quantidade em estoque:</strong> {myBook.GetQty()} unidades</p>
            </div>
            
            <p class='price'>R$ {myBook.GetPrice().ToString("F2")}</p>
            
            <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
            <p style='font-size: 0.95em; color: #555; margin-bottom: 10px;'><strong>Estrutura gerada pelo <code>toString()</code>:</strong></p>
            <ul style='list-style-type: none; padding-left: 0; background: #fdfdfd; padding: 15px; border-radius: 8px; border: 1px solid #e1e4e8; font-family: monospace; font-size: 0.9em; color: #476383;'>
                <li style='margin-bottom: 5px;'><span style='color: #d73a49;'>Book</span> [</li>
                <li style='margin-left: 20px;'><strong>name</strong> = {myBook.GetName()},</li>
                <li style='margin-left: 20px;'><strong>authors</strong> = {{ 
                    <ul style='list-style-type: none; padding-left: 20px; margin: 5px 0; color: #6f42c1;'>
                        {string.Join("", myBook.GetAuthors().Select(a => $"<li style='margin-bottom: 3px;'>{a.ToString()},</li>"))}
                    </ul>
                <span style='margin-left: 20px;'>}},</span></li>
                <li style='margin-left: 20px;'><strong>price</strong> = {myBook.GetPrice()},</li>
                <li style='margin-left: 20px;'><strong>qty</strong> = {myBook.GetQty()}</li>
                <li>]</li>
            </ul>
        </div>
    </body>
    </html>";

    return Results.Content(html, "text/html", Encoding.UTF8);
});

app.Run();
