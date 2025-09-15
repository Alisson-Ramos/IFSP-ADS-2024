using ProjetoRestauranteMvc.Model;

namespace ProjetoRestauranteMvc.Controller;

public class Restaurante
{
    private int proxPedido = 1;
    private List<Pedido> pedidos = new List<Pedido>();

    public bool NovoPedido(string cliente)
    {
        if (pedidos.Count >= 50) return false;
        pedidos.Add(new Pedido(proxPedido++, cliente));
        return true;
    }

    public Pedido BuscarPedido(int id)
    {
        return pedidos.FirstOrDefault(p => p.Id == id);
    }

    public bool CancelarPedido(int id)
    {
        var pedido = BuscarPedido(id);
        if (pedido != null)
        {
            pedidos.Remove(pedido);
            return true;
        }
        return false;
    }

    public void ListarPedidos()
    {
        double soma = 0;
        foreach (var p in pedidos)
        {
            Console.WriteLine($"Pedido {p.Id} - Cliente: {p.Cliente} - Total: R$ {p.CalcularTotal():F2}");
            soma += p.CalcularTotal();
        }
        Console.WriteLine($"Soma geral do dia: R$ {soma:F2}");
    }
    
}