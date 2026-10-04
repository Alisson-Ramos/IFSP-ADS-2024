// CBTSWE2 – TP03 – Sistema de Gerenciamento de Produtos
// Integrantes:
//   Alisson Ramos Aquino dos Santos
//   Nayara Pereira Soares

using CBTSWE2.TP03.Data;
using CBTSWE2.TP03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBTSWE2.TP03.Controllers
{
    // Camada de controle: recebe as requisições HTTP, conversa com a camada de dados
    // (ProdutoContext / Entity Framework) e escolhe qual View devolver.
    //
    // Roteamento por atributos: as URLs ficam em português e com restrição {id:int}.
    //   GET  /produtos                      -> lista (também é a página inicial "/")
    //   GET  /produtos/detalhes/5           -> detalhes (atalho: /produto/5)
    //   GET  /produtos/novo                 -> formulário de cadastro
    //   POST /produtos/novo                 -> grava o novo produto
    //   GET  /produtos/editar/5             -> formulário de edição
    //   POST /produtos/editar/5             -> grava a edição
    //   GET  /produtos/excluir/5            -> confirmação de exclusão
    //   POST /produtos/excluir/5            -> exclui
    [Route("produtos")]
    public class ProdutosController : Controller
    {
        private readonly ProdutoContext _context;

        public ProdutosController(ProdutoContext context)
        {
            _context = context;
        }

        // GET: / ou /produtos?busca=mouse&ordem=preco
        [Route("/")]
        [HttpGet("")]
        public async Task<IActionResult> Index(string? busca, string? ordem)
        {
            var consulta = _context.Produtos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                busca = busca.Trim();
                consulta = consulta.Where(p => p.Nome.Contains(busca) || p.Descricao.Contains(busca));
            }

            consulta = ordem switch
            {
                "nome_desc" => consulta.OrderByDescending(p => p.Nome),
                "preco" => consulta.OrderBy(p => p.Preco),
                "preco_desc" => consulta.OrderByDescending(p => p.Preco),
                "estoque" => consulta.OrderBy(p => p.QuantidadeEstoque),
                "estoque_desc" => consulta.OrderByDescending(p => p.QuantidadeEstoque),
                _ => consulta.OrderBy(p => p.Nome)
            };

            ViewData["Busca"] = busca;
            ViewData["Ordem"] = ordem;

            return View(await consulta.ToListAsync());
        }

        // GET: /produtos/detalhes/5  (ou o atalho /produto/5)
        [HttpGet("detalhes/{id:int}")]
        [HttpGet("/produto/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: /produtos/novo
        [HttpGet("novo")]
        public IActionResult Create()
        {
            // Sem modelo: os campos de preço e quantidade começam vazios em vez de "0".
            return View();
        }

        // POST: /produtos/novo
        // [Bind] limita os campos aceitos do formulário (proteção contra overposting).
        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
        {
            if (await NomeJaExisteAsync(produto.Nome, null))
            {
                ModelState.AddModelError(nameof(Produto.Nome), "Já existe um produto cadastrado com esse nome.");
            }

            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            produto.DataCadastro = DateTime.Now;
            _context.Add(produto);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = $"Produto \"{produto.Nome}\" cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /produtos/editar/5
        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: /produtos/editar/5
        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (await NomeJaExisteAsync(produto.Nome, id))
            {
                ModelState.AddModelError(nameof(Produto.Nome), "Já existe outro produto cadastrado com esse nome.");
            }

            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            // Carrega o registro do banco e altera só os campos editáveis,
            // preservando a data de cadastro original.
            var existente = await _context.Produtos.FindAsync(id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.Nome = produto.Nome;
            existente.Descricao = produto.Descricao;
            existente.Preco = produto.Preco;
            existente.QuantidadeEstoque = produto.QuantidadeEstoque;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProdutoExisteAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            TempData["Mensagem"] = $"Produto \"{existente.Nome}\" alterado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /produtos/excluir/5
        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: /produtos/excluir/5
        [HttpPost("excluir/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = $"Produto \"{produto.Nome}\" excluído com sucesso.";
            }

            return RedirectToAction(nameof(Index));
        }

        private Task<bool> ProdutoExisteAsync(int id)
        {
            return _context.Produtos.AnyAsync(p => p.Id == id);
        }

        private Task<bool> NomeJaExisteAsync(string? nome, int? ignorarId)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return Task.FromResult(false);
            }

            var nomeLimpo = nome.Trim();
            return _context.Produtos.AnyAsync(p => p.Nome == nomeLimpo && p.Id != ignorarId);
        }
    }
}
