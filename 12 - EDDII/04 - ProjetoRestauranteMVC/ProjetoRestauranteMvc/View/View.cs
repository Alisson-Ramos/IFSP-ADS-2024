using ProjetoRestauranteMvc.Controller;
using ProjetoRestauranteMvc.Model;

namespace ProjetoRestauranteMvc.View;

using System;

public class View
{
    private Restaurante restaurante = new Restaurante();

    public void ExibirMenu()
    {
        bool rodando = true;

        while (rodando)
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Criar novo pedido");
            Console.WriteLine("2. Adicionar item ao pedido");
            Console.WriteLine("3. Remover item do pedido");
            Console.WriteLine("4. Consultar pedido");
            Console.WriteLine("5. Cancelar pedido");
            Console.WriteLine("6. Listar todos os pedidos");
            Console.Write("Escolha: ");
            int opcao = int.Parse(Console.ReadLine() ?? "0");

            switch (opcao)
            {
                case 0:
                    rodando = false;
                    break;

                case 1:
                    CriarPedido();
                    break;

                case 2:
                    AdicionarItem();
                    break;

                case 3:
                    RemoverItem();
                    break;

                case 4:
                    ConsultarPedido();
                    break;

                case 5:
                    CancelarPedido();
                    break;

                case 6:
                    restaurante.ListarPedidos();
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private void CriarPedido()
    {
        Console.Write("Nome do cliente: ");
        string cliente = Console.ReadLine();
        if (restaurante.NovoPedido(cliente))
            Console.WriteLine("Pedido criado com sucesso!");
        else
            Console.WriteLine("Limite de pedidos atingido.");
    }

    private void AdicionarItem()
    {
        Console.Write("ID do pedido: ");
        int id = int.Parse(Console.ReadLine());
        var pedido = restaurante.BuscarPedido(id);
        if (pedido != null)
        {
            Console.Write("ID do item: ");
            int itemId = int.Parse(Console.ReadLine());
            Console.Write("Descrição do item: ");
            string desc = Console.ReadLine();
            Console.Write("Preço: ");
            double preco = double.Parse(Console.ReadLine());

            if (pedido.AdicionarItem(new Item(itemId, desc, preco)))
                Console.WriteLine("Item adicionado!");
            else
                Console.WriteLine("Pedido já tem 10 itens.");
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    
    private void RemoverItem()
    {
        Console.Write("ID do pedido: ");
        int id = int.Parse(Console.ReadLine());
        var pedido = restaurante.BuscarPedido(id);
        if (pedido != null)
        {
            Console.Write("ID do item a remover: ");
            int itemId = int.Parse(Console.ReadLine());

            // procura o item real dentro do pedido
            var item = pedido.BuscarItem(itemId);
            if (item != null && pedido.RemoverItem(item))
                Console.WriteLine("Item removido.");
            else
                Console.WriteLine("Item não encontrado.");
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    private void ConsultarPedido()
    {
        Console.Write("ID do pedido: ");
        int id = int.Parse(Console.ReadLine());
        var pedido = restaurante.BuscarPedido(id);
        if (pedido != null)
            Console.WriteLine(pedido.DadosDoPedido());
        else
            Console.WriteLine("Pedido não encontrado.");
    }

    private void CancelarPedido()
    {
        Console.Write("ID do pedido: ");
        int id = int.Parse(Console.ReadLine());
        if (restaurante.CancelarPedido(id))
            Console.WriteLine("Pedido cancelado.");
        else
            Console.WriteLine("Pedido não encontrado.");
    }
}
