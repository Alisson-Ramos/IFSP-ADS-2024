using acessos.mvc.Model;
using acessos.mvc.Model.Database;

namespace acessos.mvc.Repository
{
    public class LogRepository : ODBC
    {
        public async Task RegistrarAsync(int usuario, int ambiente, bool tipo)
        {
            await ExecuteProcedureAsync("proc_LogRegistrar",
                new() {
                new("@id_usuario", usuario.ToString()),
                new("@id_ambiente", ambiente.ToString()),
                new("@tipo", tipo.ToString())
                }
            );
        }

        public async Task<List<Log>> ListarAsync(int ambiente, int tipo)
        {
            var lista = new List<Log>();

            using var reader = await QueryProcedureAsync(
                "proc_LogListar",
                new() {
                new("@id_ambiente", ambiente.ToString()),
                new("@tipo", tipo.ToString())
                }
            );

            while (await reader.ReadAsync())
            {
                lista.Add(new Log
                {
                    DataAcesso = reader.GetDateTime(reader.GetOrdinal("dt_acesso")),
                    TipoAcesso = reader.GetBoolean(reader.GetOrdinal("tipo_acesso")),
                    Usuario = new Usuario { Id = reader.GetInt32(reader.GetOrdinal("id_usuario")) }
                });
            }

            return lista;
        }
    }

}
