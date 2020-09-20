namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoProdutoAtualizadoEvent : Event
    {
        public PedidoProdutoAtualizadoEvent(Guid clienteId, Guid pedidoId, Guid produtoId, int quantidade)
        {
            this.AggregateId = pedidoId;
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public Guid ProdutoId { get; }
        public int Quantidade { get; }
    }
}