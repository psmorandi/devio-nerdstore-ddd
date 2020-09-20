namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;
    using FluentValidation;

    public class IniciarPedidoCommand : Command
    {
        public IniciarPedidoCommand(Guid pedidoId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
            this.Total = total;
            this.NomeCartao = nomeCartao;
            this.NumeroCartao = numeroCartao;
            this.ExpiracaoCartao = expiracaoCartao;
            this.CvvCartao = cvvCartao;
        }

        public Guid ClienteId { get; }
        public string CvvCartao { get; }
        public string ExpiracaoCartao { get; }
        public string NomeCartao { get; }
        public string NumeroCartao { get; }
        public Guid PedidoId { get; }
        public decimal Total { get; }

        public override bool EhValido()
        {
            this.ValidationResult = new IniciarPedidoValidation().Validate(this);
            return this.ValidationResult.IsValid;
        }
    }

    public class IniciarPedidoValidation : AbstractValidator<IniciarPedidoCommand>
    {
        public IniciarPedidoValidation()
        {
            this.RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            this.RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");

            this.RuleFor(c => c.NomeCartao)
                .NotEmpty()
                .WithMessage("O nome no cartão não foi informado");

            this.RuleFor(c => c.NumeroCartao)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            this.RuleFor(c => c.ExpiracaoCartao)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            this.RuleFor(c => c.CvvCartao)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}