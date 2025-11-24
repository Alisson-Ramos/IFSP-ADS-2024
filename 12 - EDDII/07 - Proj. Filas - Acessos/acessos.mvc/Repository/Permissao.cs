using acessos.mvc.Model.Database;

namespace acessos.mvc.Repository
{
    public class PermissaoRepository : ODBC
    {
        public async Task ConcederAsync(int idUsuario, int idAmbiente)
        {
            await ExecuteProcedureAsync("proc_PermissaoConceder",
                new() {
                new("@id_usuario", idUsuario.ToString()),
                new("@id_ambiente", idAmbiente.ToString())
                }
            );
        }

        public async Task RevogarAsync(int idUsuario, int idAmbiente)
        {
            await ExecuteProcedureAsync("proc_PermissaoRevogar",
                new() {
                new("@id_usuario", idUsuario.ToString()),
                new("@id_ambiente", idAmbiente.ToString())
                }
            );
        }
    }

}
