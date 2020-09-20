namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    using System;

    public class PagamentoRecusadoEvent : IntegrationEvent
    {
        public PagamentoRecusadoEvent(Guid pedidoId, Guid clienteId, Guid pagamentoId, Guid transacaoId, decimal total)
        {
            this.AggregateId = pagamentoId;
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
            this.PagamentoId = pagamentoId;
            this.TransacaoId = transacaoId;
            this.Total = total;
        }

        public Guid ClienteId { get; }
        public Guid PagamentoId { get; }
        public Guid PedidoId { get; }
        public decimal Total { get; }
        public Guid TransacaoId { get; }
    }
}