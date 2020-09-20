namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;
    using FluentValidation;

    public class AdicionarItemPedidoCommand : Command
    {
        public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            this.ClienteId = clienteId;
            this.Nome = nome;
            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
            this.ValorUnitario = valorUnitario;
        }

        public Guid ClienteId { get; }
        public string Nome { get; }
        public Guid ProdutoId { get; }
        public int Quantidade { get; }
        public decimal ValorUnitario { get; }

        public override bool EhValido()
        {
            this.ValidationResult = new AdicionarItemPedidoValidation().Validate(this);

            return this.ValidationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public AdicionarItemPedidoValidation()
        {
            this.RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            this.RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            this.RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            this.RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            this.RuleFor(c => c.Quantidade)
                .LessThanOrEqualTo(15)
                .WithMessage("A quantidade máxima de um item é 15");

            this.RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}