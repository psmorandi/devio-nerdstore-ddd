namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class PedidoItemAdicionadoEvent : Event
    {
        public PedidoItemAdicionadoEvent(Guid clienteId, Guid pedidoId, Guid produtoId, string produtoNome, decimal valorUnitario, int quantidade)
        {
            this.AggregateId = pedidoId;
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
            this.ProdutoId = produtoId;
            this.ProdutoNome = produtoNome;
            this.ValorUnitario = valorUnitario;
            this.Quantidade = quantidade;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public Guid ProdutoId { get; }
        public string ProdutoNome { get; }
        public int Quantidade { get; }
        public decimal ValorUnitario { get; }
    }
}