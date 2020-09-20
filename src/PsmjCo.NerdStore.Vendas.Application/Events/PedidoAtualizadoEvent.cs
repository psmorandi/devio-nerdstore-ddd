namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoAtualizadoEvent : Event
    {
        public PedidoAtualizadoEvent(Guid clienteId, Guid pedidoId, decimal valorTotal)
        {
            this.AggregateId = pedidoId;
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
            this.ValorTotal = valorTotal;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public decimal ValorTotal { get; }
    }
}