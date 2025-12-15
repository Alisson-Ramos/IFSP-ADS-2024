using proj_API_Unlock;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    // Credenciais obtidas de lockAuthParameters.txt
    private const string CLIENT_ID = "05eb88a940484bd08e8f2f263e56eb2b";
    private const string CLIENT_SECRET = "8977e54ba82a23a016db74f4504f6b2d";
    private const string USERNAME = "ifspcbtadsedd@gmail.com";
    private const string PASSWORD_MD5 = "23e4b3eb0ac3ae12d4f9e13372b49cda";
    private const int LOCK_ID = 17097086;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Projeto Acesso ===");
            Console.WriteLine("1. Abertura Física do Dispositivo");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");

            var input = Console.ReadLine();

            if (input == "0") break;

            if (input == "1")
            {
                try
                {
                    PerformPhysicalOpeningAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar:");
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Opção inválida.");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    private static async Task PerformPhysicalOpeningAsync()
    {
        Console.WriteLine("\nIniciando processo de autenticação...");

        using (var httpClient = new HttpClient())
        {
            // 1. Autenticação (Gerar Token)
            string accessToken = await TtlockClient.LoginAsync(httpClient, CLIENT_ID, CLIENT_SECRET, USERNAME, PASSWORD_MD5);
            Console.WriteLine("Token de acesso gerado com sucesso: " + accessToken.Substring(0, 10) + "...");

            // 2. Criar cliente com o token novo
            var ttlock = new TtlockClient(httpClient, CLIENT_ID, accessToken);

            Console.WriteLine("Enviando comando de UNLOCK para a fechadura...");
            
            // 3. Abrir
            bool ok = await ttlock.UnlockAsync(LOCK_ID);

            if (ok)
            {
                Console.WriteLine("SUCESSO: Fechadura destravada fisicamente.");
            }
        }
    }
}
