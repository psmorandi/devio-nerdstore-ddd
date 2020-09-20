namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;
    using FluentValidation;

    public class AtualizarItemPedidoCommand : Command
    {
        public AtualizarItemPedidoCommand(Guid clienteId, Guid produtoId, int quantidade)
        {
            this.ClienteId = clienteId;
            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
        }

        public Guid ClienteId { get; }
        public Guid ProdutoId { get; }
        public int Quantidade { get; }

        public override bool EhValido()
        {
            this.ValidationResult = new AtualizarItemPedidoValidation().Validate(this);
            return this.ValidationResult.IsValid;
        }

        public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
        {
            public AtualizarItemPedidoValidation()
            {
                this.RuleFor(c => c.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                this.RuleFor(c => c.ProdutoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                this.RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage("A quantidade miníma de um item é 1");

                this.RuleFor(c => c.Quantidade)
                    .LessThan(15)
                    .WithMessage("A quantidade máxima de um item é 15");
            }
        }
    }
}