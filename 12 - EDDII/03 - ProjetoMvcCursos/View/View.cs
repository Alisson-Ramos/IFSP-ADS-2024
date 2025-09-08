using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMvcCursos.View
{
    public class View
    {
        public static void MostrarMenu()
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar curso");
            Console.WriteLine("2. Pesquisar curso");
            Console.WriteLine("3. Remover curso");
            Console.WriteLine("4. Adicionar disciplina no curso");
            Console.WriteLine("5. Pesquisar disciplina");
            Console.WriteLine("6. Remover disciplina do curso");
            Console.WriteLine("7. Matricular aluno na disciplina");
            Console.WriteLine("8. Remover aluno da disciplina");
            Console.WriteLine("9. Pesquisar aluno");
            Console.Write("Opção: ");
        }

        public static int LerOpcao()
        {
            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Write("Opção inválida. Digite novamente: ");
            }
            return opcao;
        }

        public static int LerInteiro(string mensagem)
        {
            Console.Write(mensagem);
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Digite novamente: ");
            }
            return valor;
        }

        public static string LerTexto(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }

        public static void MostrarMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
    }
}
