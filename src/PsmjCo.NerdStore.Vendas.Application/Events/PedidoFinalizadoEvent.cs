namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoFinalizadoEvent : Event
    {
        public PedidoFinalizadoEvent(Guid pedidoId)
        {
            this.PedidoId = pedidoId;
            this.AggregateId = pedidoId;
        }

        public Guid PedidoId { get; }
    }
}