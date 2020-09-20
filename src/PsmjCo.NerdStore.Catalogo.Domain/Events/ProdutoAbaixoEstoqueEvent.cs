namespace PsmjCo.NerdStore.Catalogo.Domain.Events
{
    using System;
    using Core.DomainObjects;
    using Core.Messages.CommonMessages.DomainEvents;

    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante)
            : base(aggregateId)
        {
            this.QuantidadeRestante = quantidadeRestante;
        }

        public int QuantidadeRestante { get; }
    }
}