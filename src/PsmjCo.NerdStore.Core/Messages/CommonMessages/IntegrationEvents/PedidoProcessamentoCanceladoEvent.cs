namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    using System;
    using DomainObjects.DTO;

    public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
    {
        public PedidoProcessamentoCanceladoEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedido produtosPedido)
        {
            this.AggregateId = pedidoId;
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
            this.ProdutosPedido = produtosPedido;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public ListaProdutosPedido ProdutosPedido { get; }
    }
}