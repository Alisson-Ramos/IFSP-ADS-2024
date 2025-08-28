using ProjetoVendedores.MVC.Model;
using System;

namespace ProjetoVendedores.MVC.Controller
{
    /// <summary>
    /// Controla a coleção de vendedores cadastrados na empresa.
    /// </summary>
    internal class Vendedores
    {
        #region Propriedades Privadas
        private Vendedor[] _osVendedores;
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Lista interna de vendedores cadastrados.
        /// </summary>
        public Vendedor[] OsVendedores
        {
            get { return _osVendedores; }
            set { _osVendedores = value; }
        }

        /// <summary>
        /// Quantidade atual de vendedores cadastrados.
        /// </summary>
        public int qtde = 0;
        #endregion

        #region Construtores
        /// <summary>
        /// Cria um repositório de vendedores.
        /// </summary>
        /// <param name="max">Número máximo de vendedores permitidos (padrão = 10).</param>
        public Vendedores(int max = 10)
        {
            OsVendedores = new Vendedor[max];
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Adiciona um novo vendedor à lista, se houver espaço disponível.
        /// </summary>
        /// <param name="v">Vendedor a ser adicionado.</param>
        /// <returns>true se o vendedor foi adicionado, false caso contrário.</returns>
        public bool AddVendedor(Vendedor v)
        {
            if (qtde < OsVendedores.Length)
            {
                OsVendedores[qtde++] = v;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Busca um vendedor pelo ID.
        /// </summary>
        /// <param name="v">Objeto vendedor com o ID preenchido.</param>
        /// <returns>O vendedor encontrado ou null se não existir.</returns>
        public Vendedor? FindVendedor(Vendedor v)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (OsVendedores[i] != null && OsVendedores[i].Equals(v))
                {
                    return OsVendedores[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Remove um vendedor da lista (se não possuir vendas registradas).
        /// </summary>
        /// <param name="v">Vendedor a ser excluído.</param>
        /// <returns>true se foi removido, false caso contrário.</returns>
        public bool DeleteVendedor(Vendedor v)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (OsVendedores[i] != null && OsVendedores[i].Id == v.Id)
                {
                    for (int j = i; j < qtde - 1; j++)
                    {
                        OsVendedores[j] = OsVendedores[j + 1];
                    }

                    OsVendedores[qtde - 1] = null;
                    qtde--;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Calcula o valor total de vendas de todos os vendedores.
        /// </summary>
        /// <returns>Soma dos valores de vendas.</returns>
        public double ValorVendas()
        {
            double soma = 0;
            for (int i = 0; i < qtde; i++)
            {
                soma += OsVendedores[i].ValorVendas();
            }
            return soma;
        }

        /// <summary>
        /// Calcula o valor total das comissões de todos os vendedores.
        /// </summary>
        /// <returns>Soma das comissões.</returns>
        public double ValorComissao()
        {
            double soma = 0;
            for (int i = 0; i < qtde; i++)
            {
                soma += OsVendedores[i].ValorComissao();
            }
            return soma;
        }
        #endregion
    }
}
