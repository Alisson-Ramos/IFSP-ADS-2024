using acessos.mvc.Controller.acessos.mvc.Controller;

public class Program
{
    private static readonly Cadastro _cadastro = new();
    

    public static async Task Main()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA DE CONTROLE DE ACESSO =====");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuário");
            Console.WriteLine("5. Consultar usuário");
            Console.WriteLine("6. Excluir usuário");
            Console.WriteLine("7. Conceder permissão");
            Console.WriteLine("8. Revogar permissão");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs");
            Console.Write("Digite a opção: ");

            opcao = int.Parse(Console.ReadLine()!);

            await ProcessarOpcao(opcao);

        } while (opcao != 0);
    }

    private static async Task ProcessarOpcao(int op)
    {
        switch (op)
        {
            case 1:
                Console.Write("Nome do ambiente: ");
                await _cadastro.AdicionarAmbiente(Console.ReadLine()!);
                break;

            case 2:
                Console.Write("ID do ambiente: ");
                int idAmb = int.Parse(Console.ReadLine()!);
                var ambiente = await _cadastro.PesquisarAmbiente(idAmb);
                Console.WriteLine(ambiente != null ?
                    $"ID: {ambiente.Id}, Nome: {ambiente.Nome}" :
                    "Ambiente não encontrado.");
                Console.ReadKey();
                break;

            case 3:
                Console.Write("ID do ambiente: ");
                idAmb = int.Parse(Console.ReadLine()!);
                Console.WriteLine(await _cadastro.RemoverAmbiente(idAmb)
                    ? "Removido." : "Não encontrado.");
                Console.ReadKey();
                break;

            case 4:
                Console.Write("Nome do usuário: ");
                await _cadastro.AdicionarUsuario(Console.ReadLine()!);
                break;

            case 5:
                Console.Write("ID do usuário: ");
                int idUser = int.Parse(Console.ReadLine()!);
                var usuario = await _cadastro.PesquisarUsuario(idUser);
                Console.WriteLine(usuario != null ?
                    $"ID: {usuario.Id}, Nome: {usuario.Nome}" :
                    "Usuário não encontrado.");
                Console.ReadKey();
                break;

            case 6:
                Console.Write("ID do usuário: ");
                idUser = int.Parse(Console.ReadLine()!);
                Console.WriteLine(await _cadastro.RemoverUsuario(idUser)
                    ? "Removido." : "Não encontrado.");
                Console.ReadKey();
                break;

            case 7:
                Console.Write("ID do usuário: ");
                idUser = int.Parse(Console.ReadLine()!);
                Console.Write("ID do ambiente: ");
                idAmb = int.Parse(Console.ReadLine()!);
                await _cadastro.ConcederPermissao(idUser, idAmb);
                break;

            case 8:
                Console.Write("ID do usuário: ");
                idUser = int.Parse(Console.ReadLine()!);
                Console.Write("ID do ambiente: ");
                idAmb = int.Parse(Console.ReadLine()!);
                await _cadastro.RevogarPermissao(idUser, idAmb);
                break;

            case 9:
                Console.Write("ID do usuário: ");
                idUser = int.Parse(Console.ReadLine()!);
                Console.Write("ID do ambiente: ");
                idAmb = int.Parse(Console.ReadLine()!);
                await _cadastro.RegistrarAcesso(idUser, idAmb);
                break;

            case 10:
                Console.Write("ID do ambiente: ");
                idAmb = int.Parse(Console.ReadLine()!);

                Console.WriteLine("1 - Autorizados | 2 - Negados | 3 - Todos");
                int tipo = int.Parse(Console.ReadLine()!);

                var logs = await _cadastro.ListarLogs(idAmb, tipo);

                foreach (var log in logs)
                    Console.WriteLine($"{log.DataAcesso} | Usuário {log.Usuario.Id} | Tipo: {log.TipoAcesso}");

                Console.ReadKey();
                break;
        }
    }
}
