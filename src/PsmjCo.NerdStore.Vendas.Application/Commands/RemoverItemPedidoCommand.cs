namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;
    using FluentValidation;

    public class RemoverItemPedidoCommand : Command
    {
        public RemoverItemPedidoCommand(Guid clienteId, Guid produtoId)
        {
            this.ClienteId = clienteId;
            this.ProdutoId = produtoId;
        }

        public Guid ClienteId { get; }
        public Guid ProdutoId { get; }

        public override bool EhValido()
        {
            this.ValidationResult = new RemoverItemPedidoCommandValidation().Validate(this);
            return this.ValidationResult.IsValid;
        }
    }

    public class RemoverItemPedidoCommandValidation : AbstractValidator<RemoverItemPedidoCommand>
    {
        public RemoverItemPedidoCommandValidation()
        {
            this.RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            this.RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    
    }
}