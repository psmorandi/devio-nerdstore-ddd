namespace PsmjCo.NerdStore.Catalogo.Application.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoriaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Codigo { get; set; }

        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
    }
}