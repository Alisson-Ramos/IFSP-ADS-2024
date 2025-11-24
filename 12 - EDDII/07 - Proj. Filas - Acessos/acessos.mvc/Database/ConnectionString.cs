using acessos.mvc.Model.Database.lib;

namespace acessos.mvc.Model.Database
{
    /// <summary>
    /// Classe Auxiliar para conexões via: ODBC, SQL SERVER, MYSQL
    /// </summary>
    internal class ConnectionString
    {
        /// <summary>
        /// Retorna Conexão do banco com base do diretório de dominio em: config/database.ini
        /// </summary>
        /// <returns></returns>
        public static String GetConnection()
        {
            return IniConfigure.GetDatabaseSettings(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
