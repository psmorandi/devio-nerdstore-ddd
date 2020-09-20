namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    using System;

    public class PedidoEstoqueRejeitadoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public PedidoEstoqueRejeitadoEvent(Guid pedidoId, Guid clienteId)
        {
            this.AggregateId = pedidoId;
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
        }
    }
}