using acessos.mvc.Model;
using acessos.mvc.Repository;

namespace acessos.mvc.Controller
{

    namespace acessos.mvc.Controller
    {
        public class Cadastro
        {
            private readonly UsuarioRepository _usuarioRepo = new();
            private readonly AmbienteRepository _ambRepo = new();
            private readonly PermissaoRepository _permRepo = new();
            private readonly LogRepository _logRepo = new();

            public async Task AdicionarUsuario(string nome)
                => await _usuarioRepo.InserirAsync(nome);

            public async Task<Usuario?> PesquisarUsuario(int id)
                => await _usuarioRepo.BuscarAsync(id);

            public async Task<bool> RemoverUsuario(int id)
                => await _usuarioRepo.RemoverAsync(id);

            public async Task AdicionarAmbiente(string nome)
                => await _ambRepo.InserirAsync(nome);

            public async Task ConcederPermissao(int idUsuario, int idAmbiente)
                => await _permRepo.ConcederAsync(idUsuario, idAmbiente);

            public async Task RevogarPermissao(int idUsuario, int idAmbiente)
                => await _permRepo.RevogarAsync(idUsuario, idAmbiente);

            public async Task RegistrarAcesso(int idUsuario, int idAmbiente)
                => await _logRepo.RegistrarAsync(idUsuario, idAmbiente, true);

            public async Task<Ambiente?> PesquisarAmbiente(int id)
                => await _ambRepo.BuscarAsync(id);

            public async Task<bool> RemoverAmbiente(int id)
                => await _ambRepo.RemoverAsync(id);

            public async Task<List<Log>> ListarLogs(int ambiente, int tipo)
                => await _logRepo.ListarAsync(ambiente, tipo);

        }

    }

}
