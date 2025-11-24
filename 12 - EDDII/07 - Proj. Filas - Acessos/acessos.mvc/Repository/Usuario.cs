using acessos.mvc.Model;
using acessos.mvc.Model.Database;
using System.Text;

namespace acessos.mvc.Repository
{
    public class UsuarioRepository : ODBC
    {
        public async Task<int> InserirAsync(string nome)
        {
            return await ExecuteProcedureAsync(
                "proc_UsuarioInserir",
                new List<Parameter> {
                new("@nome", nome)
                }
            );
        }

        public async Task<Usuario?> BuscarAsync(int id)
        {
            using var reader = await QueryProcedureAsync(
                "proc_UsuarioBuscar",
                new List<Parameter> {
                new("@id", id.ToString())
                }
            );

            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Nome = reader.GetString(reader.GetOrdinal("nome"))
                };
            }

            return null;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var reader = await QueryProcedureAsync(
                "proc_UsuarioRemover",
                new List<Parameter> { new("@id", id.ToString()) }
            );

            if (await reader.ReadAsync())
                return reader.GetInt32(0) == 1;

            return false;
        }
    }
}
