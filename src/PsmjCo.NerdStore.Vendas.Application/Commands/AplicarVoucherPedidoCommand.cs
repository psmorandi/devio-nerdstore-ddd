namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;
    using FluentValidation;

    public class AplicarVoucherPedidoCommand : Command
    {
        public AplicarVoucherPedidoCommand(Guid clienteId, string codigoVoucher)
        {
            this.ClienteId = clienteId;
            this.CodigoVoucher = codigoVoucher;
        }

        public Guid ClienteId { get; }
        public string CodigoVoucher { get; }

        public override bool EhValido()
        {
            this.ValidationResult = new AplicarVoucherPedidoValidation().Validate(this);
            return this.ValidationResult.IsValid;
        }
    }

    public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
    {
        public AplicarVoucherPedidoValidation()
        {
            this.RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            this.RuleFor(c => c.CodigoVoucher)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}