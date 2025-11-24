namespace acessos.mvc.Model.Database.lib
{

    /// <summary>
    /// Classe para ler configurações de arquivos INI e retornar configurações específicas para banco de dados e e-mail.
    /// </summary>
    public class IniConfigure
    {
        /// <summary>
        /// Lê as configurações do banco de dados a partir do arquivo database.ini.
        /// Retorna a string de conexão adequada para o ambiente (produção ou desenvolvimento).
        /// </summary>
        /// <returns>String de conexão formatada para o banco de dados.</returns>
        /// <exception cref="Exception">Lançada quando o arquivo INI não possui a chave "isProd".</exception>
        /// 
        //[Obsolete]
        public static string GetDatabaseSettings(string rootPath)
        {
            try
            {
                // Obtém o caminho raiz do site para localizar o arquivo .ini
                //string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                //string rootPath = "null";

                // Instancia a classe que lê arquivos INI (não fornecida aqui)
                IniFile ConfigureFile = new IniFile($"{rootPath}\\config\\database.ini");

                string Driver = "";
                string Server = "";
                string Database = "";
                string UserID = "";
                string Password = "";

                Driver = ConfigureFile.Read("Driver", "Settings");
                Server = ConfigureFile.Read("Server", "Settings");
                Database = ConfigureFile.Read("Database", "Settings");
                UserID = ConfigureFile.Read("UID", "Settings");
                Password = ConfigureFile.Read("PWD", "Settings");
                

                // Retorna a string de conexão para o ADO.NET
                return $"Driver={{{Driver}}};Server={Server};Database={Database};UID={UserID};PWD={Password}";
            }
            catch (Exception ex)
            {
                throw new Exception("An error ocurred:", ex);
            }
            
        }

   
    }
}