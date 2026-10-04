// CBTSWE2 – TP03 – Sistema de Gerenciamento de Produtos
// Integrantes:
//   Alisson Ramos Aquino dos Santos
//   Nayara Pereira Soares

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBTSWE2.TP03.Models
{
    // Entidade de domínio mapeada pelo Entity Framework para a tabela "Produtos".
    // As DataAnnotations servem para duas coisas ao mesmo tempo:
    //  - validação (no servidor via ModelState e no navegador via jQuery Validation);
    //  - definição do esquema do banco (tamanho das colunas, NOT NULL, tipo decimal).
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "A descrição deve ter entre 5 e 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 999.999,99.")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, 1000000, ErrorMessage = "A quantidade deve ser um número inteiro entre 0 e 1.000.000.")]
        [Display(Name = "Quantidade em estoque")]
        public int QuantidadeEstoque { get; set; }

        [Display(Name = "Cadastrado em")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Propriedades calculadas: não viram colunas no banco.
        [NotMapped]
        [Display(Name = "Valor em estoque")]
        [DataType(DataType.Currency)]
        public decimal ValorEmEstoque => Preco * QuantidadeEstoque;

        [NotMapped]
        public string SituacaoEstoque => QuantidadeEstoque switch
        {
            0 => "Esgotado",
            <= 10 => "Estoque baixo",
            _ => "Disponível"
        };
    }
}
