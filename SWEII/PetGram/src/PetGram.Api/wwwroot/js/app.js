/*
  PetGram — Rede social de animais
  Trabalho Prático 04 — CBTSWE2 — ADS 671 — IFSP Campus Cubatão
  Prof. Wellington Tuler Moraes
  Autores: Alisson Ramos Aquino dos Santos e Nayara Pereira Soares

  Página que consome a Web API (/api/pets e /api/posts) e executa o CRUD.
*/

const API = {
  pets: "/api/pets",
  posts: "/api/posts",
};

const EMOJIS = {
  cachorro: "🐶", gato: "🐱", "pássaro": "🐦", passaro: "🐦",
  coelho: "🐰", peixe: "🐟", hamster: "🐹",
};

const estado = {
  pets: [],
  posts: [],
  filtroPet: null,     // id do pet selecionado nos stories
  filtroHashtag: null, // hashtag clicada
  curtidos: carregarCurtidos(),
};

const $ = (id) => document.getElementById(id);

// ---------------------------------------------------------------- utilidades

/** Chama a API e transforma respostas de erro em exceção com mensagem amigável. */
async function api(metodo, url, corpo) {
  const opcoes = { method: metodo, headers: {} };
  if (corpo !== undefined) {
    opcoes.headers["Content-Type"] = "application/json";
    opcoes.body = JSON.stringify(corpo);
  }

  let resposta;
  try {
    resposta = await fetch(url, opcoes);
  } catch {
    throw new Error("Não foi possível conectar à API.");
  }

  if (resposta.status === 204) return null;

  const dados = await resposta.json().catch(() => null);
  if (!resposta.ok) {
    const erros = dados?.erros
      ?? (dados?.errors ? Object.values(dados.errors).flat() : null)
      ?? [dados?.title ?? `Erro ${resposta.status}`];
    const erro = new Error(erros.join(" "));
    erro.lista = erros;
    throw erro;
  }
  return dados;
}

function escapar(texto) {
  return String(texto ?? "")
    .replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;").replace(/'/g, "&#39;");
}

function emoji(especie) {
  return EMOJIS[(especie ?? "").toLowerCase()] ?? "🐾";
}

function avatarHtml(fotoUrl, especie, classe = "") {
  if (fotoUrl) {
    return `<span class="avatar ${classe}" style="background-image:url('${escapar(fotoUrl)}')"></span>`;
  }
  return `<span class="avatar ${classe}">${emoji(especie)}</span>`;
}

/** Destaca as #hashtags da legenda (o texto já vem escapado). */
function legendaHtml(legenda) {
  return escapar(legenda).replace(
    /#([\p{L}\p{N}_]+)/gu,
    (_, tag) => `<span class="hashtag" data-tag="${tag.toLowerCase()}">#${tag}</span>`
  );
}

let timerToast;
function toast(mensagem, erro = false) {
  const el = $("toast");
  el.textContent = mensagem;
  el.classList.toggle("erro", erro);
  el.classList.add("visivel");
  clearTimeout(timerToast);
  timerToast = setTimeout(() => el.classList.remove("visivel"), 3000);
}

function mostrarErros(lista, el) {
  el.innerHTML = (lista ?? []).map((e) => `<li>${escapar(e)}</li>`).join("");
}

// Curtidas deste navegador (para pintar o coração de vermelho)
function carregarCurtidos() {
  try {
    return new Set(JSON.parse(localStorage.getItem("petgram.curtidos") ?? "[]"));
  } catch {
    return new Set();
  }
}
function salvarCurtidos() {
  try {
    localStorage.setItem("petgram.curtidos", JSON.stringify([...estado.curtidos]));
  } catch { /* armazenamento indisponível: ignora */ }
}

// ---------------------------------------------------------------- carregar dados

async function carregarPets() {
  estado.pets = await api("GET", API.pets);
  renderizarStories();
  renderizarListaPets();
  preencherSelectPets();
}

async function carregarPosts() {
  const params = new URLSearchParams();
  if (estado.filtroPet) params.set("petId", estado.filtroPet);
  if (estado.filtroHashtag) params.set("hashtag", estado.filtroHashtag);

  const consulta = params.toString();
  const url = consulta ? `${API.posts}?${consulta}` : API.posts;
  estado.posts = await api("GET", url);
  renderizarFeed();
}

async function recarregarTudo() {
  try {
    await carregarPets();
    await carregarPosts();
  } catch (e) {
    toast(e.message, true);
  }
}

// ---------------------------------------------------------------- stories

function renderizarStories() {
  $("stories").innerHTML = estado.pets.map((p) => `
    <button class="story ${estado.filtroPet === p.id ? "ativo" : ""}" data-pet="${p.id}" title="@${escapar(p.usuario)}">
      <span class="anel">${avatarHtml(p.fotoUrl, p.especie)}</span>
      <span class="nome">${escapar(p.usuario)}</span>
    </button>`).join("")
    || `<p class="vazio" style="padding:8px;margin:0">Cadastre um pet para começar 🐾</p>`;
}

$("stories").addEventListener("click", (e) => {
  const botao = e.target.closest(".story");
  if (!botao) return;
  const id = Number(botao.dataset.pet);
  estado.filtroPet = estado.filtroPet === id ? null : id;
  estado.filtroHashtag = null;
  renderizarStories();
  carregarPosts().catch((err) => toast(err.message, true));
});

// ---------------------------------------------------------------- feed

function renderizarFeed() {
  const filtro = $("filtro");
  if (estado.filtroPet || estado.filtroHashtag) {
    const pet = estado.pets.find((p) => p.id === estado.filtroPet);
    $("filtroTexto").textContent = estado.filtroHashtag
      ? `Posts com #${estado.filtroHashtag}`
      : `Posts de @${pet?.usuario ?? ""}`;
    filtro.hidden = false;
  } else {
    filtro.hidden = true;
  }

  if (!estado.posts.length) {
    $("feed").innerHTML = `<div class="vazio">Nenhum post ainda. Clique em ＋ para publicar.</div>`;
    return;
  }

  $("feed").innerHTML = estado.posts.map(postHtml).join("");
}

function postHtml(p) {
  const curtido = estado.curtidos.has(p.id);
  const imagem = p.imagemUrl
    ? `<img src="${escapar(p.imagemUrl)}" alt="Foto de ${escapar(p.petNome)}" loading="lazy">`
    : `<div class="placeholder">${emoji(p.petEspecie)}</div>`;

  return `
    <article class="post" data-id="${p.id}">
      <header class="post-topo">
        ${avatarHtml(p.petFotoUrl, p.petEspecie, "pequeno")}
        <span class="usuario">${escapar(p.petUsuario)}</span>
        <span class="tempo">• ${escapar(p.tempo)}</span>
        <button class="menu" data-acao="menu" aria-label="Opções">⋯</button>
      </header>
      <div class="post-imagem" data-acao="duplo">
        ${imagem}
        <span class="coracao-grande">♥</span>
      </div>
      <div class="post-acoes">
        <button class="curtir ${curtido ? "curtido" : ""}" data-acao="curtir" aria-label="Curtir">${curtido ? "♥" : "♡"}</button>
      </div>
      <div class="post-info">
        <p class="curtidas">${p.curtidas.toLocaleString("pt-BR")} curtida${p.curtidas === 1 ? "" : "s"}</p>
        ${p.legenda ? `<p class="legenda"><strong>${escapar(p.petUsuario)}</strong>${legendaHtml(p.legenda)}</p>` : ""}
      </div>
    </article>`;
}

$("feed").addEventListener("click", async (e) => {
  const tag = e.target.closest(".hashtag");
  if (tag) {
    estado.filtroHashtag = tag.dataset.tag;
    estado.filtroPet = null;
    renderizarStories();
    carregarPosts().catch((err) => toast(err.message, true));
    return;
  }

  const artigo = e.target.closest(".post");
  if (!artigo) return;
  const id = Number(artigo.dataset.id);
  const alvo = e.target.closest("[data-acao]");
  if (!alvo) return;

  if (alvo.dataset.acao === "curtir") await alternarCurtida(id);
  if (alvo.dataset.acao === "menu") abrirMenu(artigo, id);
});

// Duplo clique na imagem curte o post, como no Instagram
$("feed").addEventListener("dblclick", async (e) => {
  const imagem = e.target.closest(".post-imagem");
  if (!imagem) return;
  const id = Number(imagem.closest(".post").dataset.id);

  const coracao = imagem.querySelector(".coracao-grande");
  coracao.classList.remove("animar");
  void coracao.offsetWidth; // reinicia a animação
  coracao.classList.add("animar");

  if (!estado.curtidos.has(id)) await alternarCurtida(id);
});

async function alternarCurtida(id) {
  const jaCurtido = estado.curtidos.has(id);
  try {
    const r = await api(jaCurtido ? "DELETE" : "POST", `${API.posts}/${id}/curtir`);
    jaCurtido ? estado.curtidos.delete(id) : estado.curtidos.add(id);
    salvarCurtidos();

    const post = estado.posts.find((p) => p.id === id);
    if (post) post.curtidas = r.curtidas;

    const artigo = document.querySelector(`.post[data-id="${id}"]`);
    if (artigo && post) artigo.outerHTML = postHtml(post);
  } catch (err) {
    toast(err.message, true);
  }
}

// Menu "⋯" com editar / excluir
function abrirMenu(artigo, id) {
  fecharMenus();
  const menu = document.createElement("div");
  menu.className = "menu-opcoes";
  menu.innerHTML = `
    <button data-op="editar">Editar</button>
    <button data-op="excluir" class="perigo">Excluir</button>`;
  artigo.appendChild(menu);

  menu.addEventListener("click", async (e) => {
    const op = e.target.dataset.op;
    fecharMenus();
    if (op === "editar") abrirModalPost(estado.posts.find((p) => p.id === id));
    if (op === "excluir") await excluirPost(id);
  });
}

function fecharMenus() {
  document.querySelectorAll(".menu-opcoes").forEach((m) => m.remove());
}

document.addEventListener("click", (e) => {
  if (!e.target.closest(".menu-opcoes") && !e.target.closest('[data-acao="menu"]')) fecharMenus();
});

async function excluirPost(id) {
  if (!confirm("Excluir este post?")) return;
  try {
    await api("DELETE", `${API.posts}/${id}`);
    estado.curtidos.delete(id);
    salvarCurtidos();
    toast("Post excluído.");
    await recarregarTudo();
  } catch (err) {
    toast(err.message, true);
  }
}

$("btnLimparFiltro").addEventListener("click", () => {
  estado.filtroPet = null;
  estado.filtroHashtag = null;
  renderizarStories();
  carregarPosts().catch((err) => toast(err.message, true));
});

// ---------------------------------------------------------------- modal de post

function preencherSelectPets() {
  $("postPet").innerHTML = estado.pets
    .map((p) => `<option value="${p.id}">${emoji(p.especie)} @${escapar(p.usuario)}</option>`)
    .join("");
}

function abrirModalPost(post) {
  if (!estado.pets.length) {
    toast("Cadastre um pet antes de publicar.", true);
    abrirModalPets();
    return;
  }

  const editando = Boolean(post);
  $("tituloModalPost").textContent = editando ? "Editar post" : "Novo post";
  $("postId").value = post?.id ?? "";
  $("postPet").value = post?.petId ?? estado.filtroPet ?? estado.pets[0].id;
  $("postPet").disabled = editando; // o dono do post não muda
  $("postImagem").value = post?.imagemUrl ?? "";
  $("postLegenda").value = post?.legenda ?? "";
  atualizarPrevia();
  atualizarContador();
  mostrarErros([], $("errosPost"));
  $("modalPost").showModal();
}

function atualizarPrevia() {
  const url = $("postImagem").value.trim();
  $("previaPost").innerHTML = /^https?:\/\//i.test(url)
    ? `<img src="${escapar(url)}" alt="Prévia" onerror="this.remove()">`
    : "";
}

function atualizarContador() {
  $("contadorLegenda").textContent = $("postLegenda").value.length.toLocaleString("pt-BR");
}

$("postImagem").addEventListener("input", atualizarPrevia);
$("postLegenda").addEventListener("input", atualizarContador);
$("btnNovoPost").addEventListener("click", () => abrirModalPost(null));

$("formPost").addEventListener("submit", async (e) => {
  e.preventDefault();
  const id = $("postId").value;
  const corpo = {
    petId: Number($("postPet").value),
    imagemUrl: $("postImagem").value.trim() || null,
    legenda: $("postLegenda").value.trim() || null,
  };

  try {
    if (id) await api("PUT", `${API.posts}/${id}`, corpo);
    else await api("POST", API.posts, corpo);

    $("modalPost").close();
    toast(id ? "Post atualizado." : "Post publicado!");
    await recarregarTudo();
  } catch (err) {
    mostrarErros(err.lista ?? [err.message], $("errosPost"));
  }
});

// ---------------------------------------------------------------- modal de pets

function abrirModalPets() {
  limparFormPet();
  $("modalPets").showModal();
}

function renderizarListaPets() {
  $("listaPets").innerHTML = estado.pets.map((p) => `
    <li>
      ${avatarHtml(p.fotoUrl, p.especie, "pequeno")}
      <div class="dados">
        <strong>@${escapar(p.usuario)}</strong>
        <small>${escapar(p.nome)} · ${escapar(p.especie)}${p.raca ? " · " + escapar(p.raca) : ""} · ${p.totalPosts} post${p.totalPosts === 1 ? "" : "s"}</small>
      </div>
      <button class="link" data-editar="${p.id}">Editar</button>
      <button class="link perigo" data-excluir="${p.id}">Excluir</button>
    </li>`).join("")
    || `<li class="vazio">Nenhum pet cadastrado.</li>`;
}

$("listaPets").addEventListener("click", async (e) => {
  const editar = e.target.dataset.editar;
  const excluir = e.target.dataset.excluir;

  if (editar) {
    const p = estado.pets.find((x) => x.id === Number(editar));
    $("tituloFormPet").textContent = `Editando @${p.usuario}`;
    $("petId").value = p.id;
    $("petNome").value = p.nome;
    $("petUsuario").value = p.usuario;
    $("petEspecie").value = p.especie;
    $("petRaca").value = p.raca ?? "";
    $("petBio").value = p.bio ?? "";
    $("petFoto").value = p.fotoUrl ?? "";
    $("btnCancelarPet").hidden = false;
    mostrarErros([], $("errosPet"));
    $("petNome").focus();
  }

  if (excluir) {
    const p = estado.pets.find((x) => x.id === Number(excluir));
    if (!confirm(`Excluir @${p.usuario} e todos os posts dele?`)) return;
    try {
      await api("DELETE", `${API.pets}/${p.id}`);
      if (estado.filtroPet === p.id) estado.filtroPet = null;
      toast("Pet excluído.");
      limparFormPet();
      await recarregarTudo();
    } catch (err) {
      toast(err.message, true);
    }
  }
});

function limparFormPet() {
  $("formPet").reset();
  $("petId").value = "";
  $("tituloFormPet").textContent = "Novo pet";
  $("btnCancelarPet").hidden = true;
  mostrarErros([], $("errosPet"));
}

$("btnCancelarPet").addEventListener("click", limparFormPet);
$("btnPets").addEventListener("click", abrirModalPets);

$("formPet").addEventListener("submit", async (e) => {
  e.preventDefault();
  const id = $("petId").value;
  const corpo = {
    nome: $("petNome").value.trim(),
    usuario: $("petUsuario").value.trim(),
    especie: $("petEspecie").value,
    raca: $("petRaca").value.trim() || null,
    bio: $("petBio").value.trim() || null,
    fotoUrl: $("petFoto").value.trim() || null,
  };

  try {
    if (id) await api("PUT", `${API.pets}/${id}`, corpo);
    else await api("POST", API.pets, corpo);

    toast(id ? "Pet atualizado." : "Pet cadastrado!");
    limparFormPet();
    await recarregarTudo();
  } catch (err) {
    mostrarErros(err.lista ?? [err.message], $("errosPet"));
  }
});

// Botões "Cancelar" / "Fechar" dos modais
document.querySelectorAll("[data-fechar]").forEach((b) =>
  b.addEventListener("click", () => b.closest("dialog").close())
);

// ---------------------------------------------------------------- início
recarregarTudo();
