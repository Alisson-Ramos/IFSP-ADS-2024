# TP03 – Sistema de Gerenciamento de Produtos

**CBTSWE2 – ADS 671 – IFSP Campus Cubatão** · Prof. Wellington Tuler Moraes

## Integrantes

- Alisson Ramos Aquino dos Santos
- Nayara Pereira Soares

Aplicação ASP.NET Core MVC (.NET 8) com Entity Framework Core 8 e SQL Server para
cadastrar, listar, detalhar, editar e excluir produtos.

## Como executar

1. Abra `CBTSWE2.TP03.sln` no Visual Studio 2022 (ou rode `dotnet run` na pasta do projeto).
2. Pressione **F5**. Na primeira execução o banco `CBTSWE2_TP03_Produtos` é criado
   automaticamente no **SQL Server LocalDB** (a migration é aplicada no `Program.cs`) e já vem
   com 4 produtos de exemplo.

Para usar outra instância do SQL Server (ex.: `.\SQLEXPRESS`), altere a connection string
`ProdutoContext` em `appsettings.json`:

```json
"ProdutoContext": "Server=.\\SQLEXPRESS;Database=CBTSWE2_TP03_Produtos;Trusted_Connection=True;TrustServerCertificate=True"
```

Se preferir criar o banco manualmente: **Ferramentas → Gerenciador de Pacotes NuGet → Console**
e execute `Update-Database`.

## Estrutura (MVC)

| Pasta / arquivo | Papel |
|---|---|
| `Models/Produto.cs` | Entidade de domínio + regras de validação (DataAnnotations) |
| `Data/ProdutoContext.cs` | `DbContext` do Entity Framework (mapeamento e dados iniciais) |
| `Migrations/` | Migration `CriacaoInicial` que cria a tabela `Produtos` |
| `Controllers/ProdutosController.cs` | Camada de controle: CRUD, busca e ordenação |
| `Controllers/HomeController.cs` | Página "Sobre" e página de erro/404 |
| `Views/Produtos/` | Telas: lista, cadastro, edição, detalhes, exclusão e formulário compartilhado |
| `Views/Shared/_Layout.cshtml` | Layout com menu, mensagens de sucesso e rodapé |
| `wwwroot/css/site.css` | Estilos da aplicação (sobre o Bootstrap 5) |

## Requisitos atendidos

- **Página inicial com a lista de produtos** (`/`), com busca por nome/descrição, ordenação por
  nome, preço e estoque, e resumo (quantidade de produtos, itens e valor total em estoque).
- **Cadastro** com nome, descrição, preço e quantidade em estoque (`/produtos/novo`).
- **Edição** (`/produtos/editar/{id}`) e **exclusão** com confirmação (`/produtos/excluir/{id}`).
- **Detalhes** (`/produtos/detalhes/{id}` ou `/produto/{id}`), com valor em estoque, situação
  do estoque e data de cadastro.
- **Validação** no navegador (jQuery Validation) e no servidor (`ModelState`):
  campos obrigatórios, tamanho do nome (3–100) e da descrição (5–500), preço entre
  R$ 0,01 e R$ 999.999,99, quantidade inteira entre 0 e 1.000.000 e nome sem duplicidade.
  Preço aceito no formato brasileiro (`1.234,56`).
- **Entity Framework Core**: `DbContext`, `DbSet<Produto>`, migrations, consultas LINQ assíncronas.
- **Roteamento**: rotas por atributo em português com restrição `{id:int}`
  (`[Route("produtos")]`, `[HttpGet("editar/{id:int}")]`…), rota convencional
  `{controller}/{action}/{id?}` para o `HomeController`, URLs em minúsculas e página amigável
  para 404.
- **Estilo**: Bootstrap 5, Bootstrap Icons e CSS próprio (cartões de resumo, tabela, selos de
  situação do estoque, alertas de sucesso).

## Roteiro sugerido para o vídeo

1. Abrir a aplicação: mostrar a lista inicial, os cartões de resumo e as cores do estoque.
2. Buscar "mouse" e ordenar por preço (clicar no cabeçalho).
3. **Novo produto**: clicar em *Salvar* com tudo vazio (mensagens de obrigatório), digitar nome
   com 2 letras e preço negativo (mensagens de faixa), depois preencher certo
   (ex.: preço `1.234,56`) e salvar → mensagem de sucesso.
4. Tentar cadastrar outro produto com o mesmo nome → erro de nome duplicado (validação no servidor).
5. **Detalhes** do produto criado (mostrar também a URL curta `/produto/{id}`).
6. **Editar** preço/quantidade e salvar.
7. **Excluir** com a tela de confirmação.
8. Acessar `/produtos/detalhes/999` → página de "não encontrado".
9. Mostrar rapidamente o código: `Produto.cs`, `ProdutoContext.cs`, `ProdutosController.cs`,
   a migration e a tabela no SQL Server Object Explorer.
