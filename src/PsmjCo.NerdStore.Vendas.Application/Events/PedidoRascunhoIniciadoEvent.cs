namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoRascunhoIniciadoEvent : Event
    {
        public PedidoRascunhoIniciadoEvent(Guid clienteId, Guid pedidoId)
        {
            this.AggregateId = pedidoId;
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
    }
}