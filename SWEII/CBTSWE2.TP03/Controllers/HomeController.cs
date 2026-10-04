// CBTSWE2 – TP03 – Sistema de Gerenciamento de Produtos
// Integrantes:
//   Alisson Ramos Aquino dos Santos
//   Nayara Pereira Soares

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CBTSWE2.TP03.Models;

namespace CBTSWE2.TP03.Controllers;

// Usa a rota convencional definida no Program.cs: /home/{action}/{id?}
public class HomeController : Controller
{
    // GET: /home/sobre
    public IActionResult Sobre()
    {
        return View();
    }

    // GET: /home/status/404 (chamado pelo UseStatusCodePagesWithReExecute)
    public IActionResult Status(int id)
    {
        Response.StatusCode = id;
        ViewData["Codigo"] = id;
        return View();
    }

    // GET: /home/erro (chamado pelo UseExceptionHandler fora do ambiente de desenvolvimento)
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Erro()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
