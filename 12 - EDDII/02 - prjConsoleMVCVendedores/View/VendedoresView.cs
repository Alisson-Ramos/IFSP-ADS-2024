using ProjetoVendedores.MVC.Model;
using ProjetoVendedores.MVC.Controller;
using System;

namespace ProjetoVendedores.MVC.View
{
    public class VendedoresView
    {
        private Vendedores vendedores;

        public VendedoresView()
        {
            vendedores = new Vendedores(10);
        }

        public void Init()
        {
            int opcao = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("=======================================");
                Console.WriteLine("   SISTEMA DE GERENCIAMENTO DE VENDAS  ");
                Console.WriteLine("=======================================");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.Write("Digite sua opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o programa...");
                        break;
                    case 1:
                        CadastrarVendedor();
                        break;
                    case 2:
                        ConsultarVendedor();
                        break;
                    case 3:
                        ExcluirVendedor();
                        break;
                    case 4:
                        RegistrarVenda();
                        break;
                    case 5:
                        ListarVendedores();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void CadastrarVendedor()
        {
            Console.Write("Digite o ID do vendedor: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do vendedor: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o percentual de comissão: ");
            double perc = double.Parse(Console.ReadLine());

            Vendedor v = new Vendedor(id, nome, perc);

            if (vendedores.AddVendedor(v))
                Console.WriteLine("Vendedor cadastrado com sucesso!");
            else
                Console.WriteLine("Limite máximo de vendedores atingido (10).");
        }

        private void ConsultarVendedor()
        {
            Console.Write("Digite o ID do vendedor: ");
            int id = int.Parse(Console.ReadLine());
            Vendedor v = vendedores.FindVendedor(new Vendedor(id, "", 0));

            if (v != null)
            {
                Console.WriteLine($"ID: {v.Id}");
                Console.WriteLine($"Nome: {v.Nome}");
                Console.WriteLine($"Total Vendas: {v.ValorVendas():C}");
                Console.WriteLine($"Comissão: {v.ValorComissao():C}");

                double somaMedias = 0;
                int diasComVenda = 0;
                foreach (var venda in v.GetType()
                    .GetField("_asVendas", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .GetValue(v) as Venda[])
                {
                    if (venda != null)
                    {
                        somaMedias += venda.Valor / venda.Qtde;
                        diasComVenda++;
                    }
                }

                if (diasComVenda > 0)
                    Console.WriteLine($"Valor médio diário: {somaMedias / diasComVenda:C}");
                else
                    Console.WriteLine("Nenhuma venda registrada.");
            }
            else
            {
                Console.WriteLine("Vendedor não encontrado.");
            }
        }

        private void ExcluirVendedor()
        {
            Console.Write("Digite o ID do vendedor a excluir: ");
            int id = int.Parse(Console.ReadLine());
            Vendedor v = vendedores.FindVendedor(new Vendedor(id, "", 0));

            if (v != null)
            {
                if (v.ValorVendas() == 0)
                {
                    if (vendedores.DeleteVendedor(v))
                        Console.WriteLine("Vendedor excluído com sucesso.");
                }
                else
                {
                    Console.WriteLine("O vendedor possui vendas registradas e não pode ser excluído.");
                }
            }
            else
            {
                Console.WriteLine("Vendedor não encontrado.");
            }
        }

        private void RegistrarVenda()
        {
            Console.Write("Digite o ID do vendedor: ");
            int id = int.Parse(Console.ReadLine());
            Vendedor v = vendedores.FindVendedor(new Vendedor(id, "", 0));

            if (v != null)
            {
                Console.Write("Digite o dia da venda (1 a 31): ");
                int dia = int.Parse(Console.ReadLine());

                Console.Write("Digite a quantidade vendida: ");
                int qtde = int.Parse(Console.ReadLine());

                Console.Write("Digite o valor total da venda: ");
                double valor = double.Parse(Console.ReadLine());

                Venda venda = new Venda(qtde, valor);
                v.RegistrarVenda(dia, venda);

                Console.WriteLine("Venda registrada com sucesso!");
            }
            else
            {
                Console.WriteLine("Vendedor não encontrado.");
            }
        }

        private void ListarVendedores()
        {
            Console.WriteLine("\n=== LISTA DE VENDEDORES ===");
            double totalVendas = 0;
            double totalComissao = 0;

            for (int i = 0; i < vendedores.qtde; i++)
            {
                Vendedor v = vendedores.OsVendedores[i];
                if (v != null)
                {
                    double vendas = v.ValorVendas();
                    double comissao = v.ValorComissao();

                    Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome} | Vendas: {vendas:C} | Comissão: {comissao:C}");

                    totalVendas += vendas;
                    totalComissao += comissao;
                }
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"TOTAL VENDAS: {totalVendas:C}");
            Console.WriteLine($"TOTAL COMISSÕES: {totalComissao:C}");
        }
    }
}
