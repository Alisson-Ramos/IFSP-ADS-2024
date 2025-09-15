using System.ComponentModel.DataAnnotations;

namespace ProjetoRestauranteMvc.Model;

public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; }
    private List<Item> Itens { get; set; }

    public Pedido(int id, string cliente)
    {
        Id = id;
        Cliente = cliente;
        Itens = new List<Item>();
    }

    public bool AdicionarItem(Item item)
    {
        if (Itens.Count >= 10) return false;
        Itens.Add(item);
        return true;
    }

    public bool RemoverItem(Item item)
    {
        return Itens.Remove(item);
    }

    public double CalcularTotal()
    {
        return Itens.Sum(i => i.Preco);
    }

    public string DadosDoPedido()
    {
        string dados = $"Pedido {Id} - Cliente: {Cliente}\n";
        foreach (var item in Itens)
        {
            dados += $"   {item}\n";
        }
        dados += $"Total: R$ {CalcularTotal():F2}";
        return dados;
    }
    public Item BuscarItem(int id)
    {
        return Itens.FirstOrDefault(i => i.Id == id);
    }
    
}