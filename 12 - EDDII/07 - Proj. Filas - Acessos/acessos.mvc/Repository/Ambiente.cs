using acessos.mvc.Model;
using acessos.mvc.Model.Database;

namespace acessos.mvc.Repository
{
    public class AmbienteRepository : ODBC
    {
        public async Task<int> InserirAsync(string nome)
        {
            return await ExecuteProcedureAsync(
                "proc_AmbienteInserir",
                new List<Parameter> {
                new("@nome", nome)
                }
            );
        }

        public async Task<Ambiente?> BuscarAsync(int id)
        {
            using var reader = await QueryProcedureAsync(
                "proc_AmbienteBuscar",
                new List<Parameter> {
                new("@id", id.ToString())
                }
            );

            if (await reader.ReadAsync())
            {
                return new Ambiente
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
                "proc_AmbienteRemover",
                new List<Parameter> { new("@id", id.ToString()) }
            );

            if (await reader.ReadAsync())
                return reader.GetInt32(0) == 1;

            return false;
        }
    }
}
