using System.Data;
using System.Data.Odbc;
using System.Data.Common;

namespace acessos.mvc.Model.Database
{
    /// <summary>
    /// Classe base responsável por fornecer suporte a conexão e execução de comandos
    /// via ODBC.  
    ///  
    /// Ela abstrai a lógica de gerenciamento de conexões,
    /// execução de SQL, execução de Procedures e consultas,
    /// permitindo que repositórios concretos utilizem apenas métodos já prontos.
    /// </summary>
    public abstract class ODBC
    {
        /// <summary>
        /// String de conexão obtida da camada de configuração.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Construtor do ODBC que carrega a connection string usada
        /// por todas as operações da classe.
        /// </summary>
        public ODBC()
        {
            _connectionString = ConnectionString.GetConnection();
        }

        /// <summary>
        /// Cria uma nova conexão ODBC, abre e devolve pronta para uso.
        /// Cada chamada retorna uma nova instância de OdbcConnection.
        /// </summary>
        /// <returns>Instância aberta de OdbcConnection.</returns>
        protected async Task<OdbcConnection> CreateConnectionAsync()
        {
            var conn = new OdbcConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }

        /// <summary>
        /// Executa um comando SQL simples (INSERT, UPDATE, DELETE).
        /// Retorna o número de linhas afetadas.
        /// </summary>
        /// <param name="sql">String contendo o comando SQL.</param>
        protected async Task<int> ExecuteAsync(string sql)
        {
            using var conn = await CreateConnectionAsync();
            using var cmd = new OdbcCommand(sql, conn);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executa uma procedure no banco de dados passando parâmetros.
        /// Retorna o número de linhas afetadas.
        /// </summary>
        /// <param name="proc">Nome da procedure.</param>
        /// <param name="parameters">Lista de parâmetros na ordem correta.</param>
        protected async Task<int> ExecuteProcedureAsync(string proc, List<Parameter> parameters)
        {
            using var conn = await CreateConnectionAsync();

            using var cmd = new OdbcCommand(BuildProcedureCall(proc, parameters.Count), conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Adiciona parâmetros na ordem definida no método BuildProcedureCall
            foreach (var p in parameters)
                cmd.Parameters.AddWithValue(p.Name, p.Value);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna um DbDataReader.
        /// O reader fecha automaticamente a conexão ao ser fechado pelo cliente.
        /// </summary>
        /// <param name="sql">Query SQL SELECT.</param>
        /// <returns>DbDataReader contendo os resultados.</returns>
        protected async Task<DbDataReader> QueryAsync(string sql)
        {
            var conn = await CreateConnectionAsync();
            using var cmd = new OdbcCommand(sql, conn);

            // Usa CloseConnection para liberar a conexão ao fechar o reader
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Executa uma procedure que retorna dados (SELECT interno).
        /// Retorna um DbDataReader com os resultados.
        /// </summary>
        /// <param name="proc">Nome da procedure.</param>
        /// <param name="parameters">Lista de parâmetros ordenados.</param>
        /// <returns>DbDataReader com os dados retornados.</returns>
        protected async Task<DbDataReader> QueryProcedureAsync(string proc, List<Parameter> parameters)
        {
            var conn = await CreateConnectionAsync();

            var cmd = new OdbcCommand(BuildProcedureCall(proc, parameters.Count), conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var p in parameters)
                cmd.Parameters.AddWithValue(p.Name, p.Value);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Monta a chamada ODBC de uma procedure no formato:
        /// {CALL procName(?, ?, ?)}
        /// </summary>
        /// <param name="name">Nome da procedure.</param>
        /// <param name="count">Quantidade de parâmetros.</param>
        private string BuildProcedureCall(string name, int count)
        {
            if (count == 0)
                return $"{{CALL {name}()}}";

            string placeholders = string.Join(",", Enumerable.Repeat("?", count));
            return $"{{CALL {name}({placeholders})}}";
        }
    }
}
