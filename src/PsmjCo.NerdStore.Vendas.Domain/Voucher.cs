namespace PsmjCo.NerdStore.Vendas.Domain
{
    using System;
    using System.Collections.Generic;
    using Core.DomainObjects;
    using FluentValidation;
    using FluentValidation.Results;

    public class Voucher : Entity
    {
        public bool Ativo { get; }
        public string Codigo { get; }
        public DateTime DataCadastro { get; }
        public DateTime? DataUtilizacao { get; }
        public DateTime DataValidade { get; }

        // EF. Relation
        public ICollection<Pedido> Pedidos { get; set; }
        public decimal? Percentual { get; }
        public int Quantidade { get; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; }
        public bool Utilizado { get; }
        public decimal? ValorDesconto { get; }

        internal ValidationResult VerficarSeAplicavel()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public VoucherAplicavelValidation()
        {
            this.RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Este voucher está expirado.");

            this.RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            this.RuleFor(c => c.Utilizado)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            this.RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.UtcNow;
        }
    }
}