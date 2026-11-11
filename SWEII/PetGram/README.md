# 🐾 PetGram — Rede social de animais

**Trabalho Prático 04 — Desenvolvimento de Web API com ASP.NET Core e CRUD em Página Web**
CBTSWE2 – ADS 671 · IFSP Campus Cubatão · Prof. Wellington Tuler Moraes

**Autores:**
- Alisson Ramos Aquino dos Santos
- Nayara Pereira Soares

---

## Sobre o projeto

O PetGram é uma rede social de animais inspirada no Instagram. Cada pet tem um perfil (nome, @usuário, espécie, raça, bio e foto) e pode publicar posts com imagem e legenda. O feed mostra os posts do mais recente para o mais antigo, com curtidas, hashtags clicáveis e o tempo da publicação ("há 5 min", "há 2 h").

| Requisito do enunciado | Como foi atendido |
|---|---|
| Ambiente ASP.NET Core | .NET 8, solução `PetGram.sln` com 3 projetos |
| Banco de dados local | SQLite (`petgram.db`, criado automaticamente) |
| Web API com CRUD | `PetsController` e `PostsController` (GET, POST, PUT, DELETE) |
| Integração com DLL | Biblioteca de classes `PetGram.Core.dll` com as regras de negócio |
| ORM | Entity Framework Core 8 |
| Página web com formulários | `wwwroot/index.html` + `js/app.js` (feed, stories, modais de post e de pets) |
| Testes unitários | Projeto `PetGram.Tests` (xUnit) com testes da DLL e dos controllers |
| Tratamento de exceções | Middleware `TratamentoExcecoesMiddleware` + validações com mensagens claras |

## Tecnologias

- C# / .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8 + SQLite
- Swagger (Swashbuckle) para documentação da API
- HTML, CSS e JavaScript puro (Fetch API)
- xUnit + EF Core InMemory para testes

## Estrutura

```
PetGram/
├── PetGram.sln
├── src/
│   ├── PetGram.Core/              ← DLL com regras de negócio
│   │   ├── HashtagExtractor.cs     extrai #hashtags da legenda
│   │   ├── PetValidator.cs         valida nome, @usuário, espécie, bio e URL da foto
│   │   ├── PostValidator.cs        valida legenda (2.200 caracteres, 30 hashtags) e URL da imagem
│   │   ├── TempoRelativo.cs        "agora", "há 5 min", "há 2 h", "há 3 d"
│   │   └── ResultadoValidacao.cs
│   └── PetGram.Api/               ← Web API + página web
│       ├── Controllers/            PetsController, PostsController
│       ├── Data/                   AppDbContext (EF Core) e SeedData
│       ├── Dtos/                   objetos de entrada e saída da API
│       ├── Middleware/             tratamento global de exceções
│       ├── Models/                 entidades Pet e Post
│       └── wwwroot/                index.html, css/style.css, js/app.js
├── tests/
│   └── PetGram.Tests/             ← testes unitários (xUnit)
└── docs/screenshots/              ← capturas de tela da página
```

## Como executar

Pré-requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
git clone https://github.com/<usuario>/PetGram.git
cd PetGram
dotnet restore
dotnet run --project src/PetGram.Api
```

Abra **http://localhost:5080** no navegador. O banco `petgram.db` é criado na primeira execução com 3 pets e 4 posts de exemplo.

A documentação interativa da API fica em **http://localhost:5080/swagger**.

No Visual Studio: abra `PetGram.sln`, defina `PetGram.Api` como projeto de inicialização e pressione F5.

## Endpoints da API

### Pets — `/api/pets`

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/pets` | Lista os pets |
| GET | `/api/pets/{id}` | Busca um pet |
| POST | `/api/pets` | Cria um pet |
| PUT | `/api/pets/{id}` | Atualiza um pet |
| DELETE | `/api/pets/{id}` | Exclui o pet e os posts dele |

Exemplo de corpo (POST/PUT):

```json
{
  "nome": "Thor",
  "usuario": "thor.caramelo",
  "especie": "Cachorro",
  "raca": "Vira-lata caramelo",
  "bio": "Fã de bolinha",
  "fotoUrl": null
}
```

### Posts — `/api/posts`

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/posts?petId=1&hashtag=praia` | Feed (filtros opcionais) |
| GET | `/api/posts/{id}` | Busca um post |
| POST | `/api/posts` | Publica um post |
| PUT | `/api/posts/{id}` | Edita imagem/legenda |
| DELETE | `/api/posts/{id}` | Exclui um post |
| POST | `/api/posts/{id}/curtir` | Curte (+1) |
| DELETE | `/api/posts/{id}/curtir` | Remove a curtida (−1) |

Exemplo de corpo (POST/PUT):

```json
{
  "petId": 1,
  "imagemUrl": "https://exemplo.com/foto.jpg",
  "legenda": "Dia de praia! #praia #dog"
}
```

### Respostas de erro

| Código | Quando |
|---|---|
| 400 | Dados inválidos (ex.: `{"erros": ["O nome do pet é obrigatório."]}`) |
| 404 | Pet ou post não encontrado |
| 409 | @usuário já em uso |
| 500 | Erro inesperado (capturado pelo middleware, sem expor detalhes internos) |

## A DLL `PetGram.Core`

As regras que não dependem de banco nem de HTTP ficam em uma biblioteca de classes separada, compilada como `PetGram.Core.dll` e referenciada pela API:

- **`PetValidator` / `PostValidator`** — usados pelos controllers antes de salvar. Seguem os limites do Instagram: usuário com 3–30 caracteres (letras minúsculas, números, ponto e sublinhado), legenda de até 2.200 caracteres e no máximo 30 hashtags.
- **`HashtagExtractor`** — gera a lista `hashtags` de cada post e permite o filtro `?hashtag=` do feed.
- **`TempoRelativo`** — gera o campo `tempo` ("há 2 h") mostrado no topo de cada post.

Assim a mesma regra vale para a página, para o Swagger e para qualquer outro cliente da API, e pode ser testada isoladamente.

## Página web

- **Stories:** cada pet aparece em um círculo; clicar filtra o feed pelos posts dele.
- **Feed:** posts com foto (ou um emoji da espécie quando não há imagem), curtidas, legenda e hashtags.
- **Curtir:** botão de coração ou duplo clique na imagem, como no Instagram.
- **Hashtags:** clicar em uma #hashtag filtra o feed.
- **Novo post (＋):** formulário com seleção do pet, URL da imagem (com prévia) e legenda com contador de caracteres.
- **Menu ⋯ do post:** editar ou excluir.
- **Gerenciar pets (ícone de perfil):** lista com editar/excluir e formulário de cadastro.
- Erros de validação da API aparecem em vermelho dentro do formulário.

## Testes

```bash
dotnet test
```

- `CoreTests` — testa a DLL: extração de hashtags (inclusive com acento), validação de pets e posts, normalização do @usuário e tempo relativo.
- `ApiTests` — testa os controllers com banco em memória: criação (201), dados inválidos (400), usuário duplicado (409), registro inexistente (404), atualização, exclusão, filtro por hashtag e curtidas que não ficam negativas.

## Capturas de tela

> Adicionar as imagens em `docs/screenshots/` e atualizar os links abaixo.

| Tela | Captura |
|---|---|
| Feed | `![Feed](docs/screenshots/feed.png)` |
| Novo post | `![Novo post](docs/screenshots/novo-post.png)` |
| Gerenciar pets | `![Pets](docs/screenshots/pets.png)` |
| Swagger | `![Swagger](docs/screenshots/swagger.png)` |

## Vídeo de demonstração

> Link do vídeo: _adicionar aqui_
