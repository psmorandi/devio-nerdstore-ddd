namespace PsmjCo.NerdStore.Catalogo.Application.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProdutoViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }

        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Imagem { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Largura { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Profundidade { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
    }
}