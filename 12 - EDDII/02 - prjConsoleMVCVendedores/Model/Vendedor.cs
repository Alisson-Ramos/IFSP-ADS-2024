using System;

namespace ProjetoVendedores.MVC.Model
{
    /// <summary>
    /// Representa um vendedor com suas informações de identificação,
    /// percentual de comissão e registros de vendas diárias.
    /// </summary>
    public class Vendedor
    {
        #region Propriedades Privadas
        private int _id;
        private string _nome;
        private double _percComissao;
        private Venda[] _asVendas;
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Identificador único do vendedor.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { if (value > 0) _id = value; }
        }

        /// <summary>
        /// Nome completo do vendedor.
        /// </summary>
        public string Nome
        {
            get { return _nome; }
            set { if (!string.IsNullOrEmpty(value)) _nome = value; }
        }

        /// <summary>
        /// Percentual de comissão do vendedor (em %).
        /// </summary>
        public double PercComissao
        {
            get { return _percComissao; }
            set { if (value > 0) _percComissao = value; }
        }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão. Inicializa o vetor de vendas com 31 posições (dias do mês).
        /// </summary>
        public Vendedor()
        {
            _asVendas = new Venda[31];
        }

        /// <summary>
        /// Construtor sobrecarregado para criar um vendedor com valores definidos.
        /// </summary>
        /// <param name="id">Identificador do vendedor.</param>
        /// <param name="nome">Nome do vendedor.</param>
        /// <param name="percComissao">Percentual de comissão (em %).</param>
        public Vendedor(int id, string nome, double percComissao) : this()
        {
            Id = id;
            Nome = nome;
            PercComissao = percComissao;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Registra uma venda em um dia específico.
        /// </summary>
        /// <param name="dia">Dia do mês (1 a 31).</param>
        /// <param name="venda">Objeto Venda com quantidade e valor.</param>
        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia < 1 || dia > 31)
                throw new ArgumentOutOfRangeException(nameof(dia), "Dia deve estar entre 1 e 31.");

            _asVendas[dia - 1] = venda;
        }

        /// <summary>
        /// Retorna o valor total das vendas realizadas pelo vendedor.
        /// </summary>
        /// <returns>Soma dos valores das vendas.</returns>
        public double ValorVendas()
        {
            double soma = 0;
            foreach (Venda venda in _asVendas)
            {
                if (venda != null)
                    soma += venda.Valor;
            }
            return soma;
        }

        /// <summary>
        /// Calcula o valor total da comissão com base no percentual definido.
        /// </summary>
        /// <returns>Valor da comissão.</returns>
        public double ValorComissao()
        {
            return ValorVendas() * (PercComissao / 100);
        }

        /// <summary>
        /// Calcula o valor médio diário das vendas (considera apenas dias com vendas registradas).
        /// </summary>
        /// <returns>Valor médio diário ou 0 caso não haja vendas.</returns>
        public double ValorMedioDiario()
        {
            double soma = 0;
            int diasComVenda = 0;

            foreach (Venda venda in _asVendas)
            {
                if (venda != null)
                {
                    soma += venda.Valor / venda.Qtde;
                    diasComVenda++;
                }
            }

            return diasComVenda > 0 ? soma / diasComVenda : 0;
        }

        /// <summary>
        /// Indica se o vendedor possui alguma venda registrada.
        /// </summary>
        /// <returns>true se houver vendas, false caso contrário.</returns>
        public bool PossuiVendas()
        {
            foreach (Venda venda in _asVendas)
            {
                if (venda != null)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Compara dois vendedores pelo seu ID.
        /// </summary>
        /// <param name="obj">Outro objeto vendedor para comparação.</param>
        /// <returns>true se os IDs forem iguais, false caso contrário.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vendedor vendedor)
                return Id == vendedor.Id;
            return false;
        }

        /// <summary>
        /// Retorna o hash code baseado no ID do vendedor.
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
