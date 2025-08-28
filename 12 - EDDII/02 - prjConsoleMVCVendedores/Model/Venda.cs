using System;

namespace ProjetoVendedores.MVC.Model
{
    /// <summary>
    /// Representa uma venda realizada por um vendedor,
    /// contendo quantidade e valor total.
    /// </summary>
    public class Venda
    {
        #region Propriedades Privadas
        private int _qtde;
        private double _valor;
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Quantidade de itens vendidos.
        /// </summary>
        public int Qtde
        {
            get { return _qtde; }
            set { if (value > 0) _qtde = value; }
        }

        /// <summary>
        /// Valor total da venda.
        /// </summary>
        public double Valor
        {
            get { return _valor; }
            set { if (value > 0) _valor = value; }
        }
        #endregion

        #region Construtores
        /// <summary>
        /// Cria uma venda com quantidade e valor total.
        /// </summary>
        /// <param name="qtde">Quantidade de itens vendidos.</param>
        /// <param name="valor">Valor total da venda.</param>
        public Venda(int qtde, double valor)
        {
            Qtde = qtde;
            Valor = valor;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Calcula o valor médio por item vendido.
        /// </summary>
        /// <returns>Valor médio unitário da venda.</returns>
        public double ValorMedio()
        {
            if (Qtde == 0)
                return 0;
            return Valor / Qtde;
        }
        #endregion
    }
}
