namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoProdutoRemovidoEvent : Event
    {
        public PedidoProdutoRemovidoEvent(Guid clienteId, Guid pedidoId, Guid produtoId)
        {
            this.AggregateId = pedidoId;
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
            this.ProdutoId = produtoId;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public Guid ProdutoId { get; }
    }
}