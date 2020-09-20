namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System;
    using Core.Messages;

    public class CancelarProcessamentoPedidoEstornarEstoqueCommand : Command
    {
        public CancelarProcessamentoPedidoEstornarEstoqueCommand(Guid pedidoId, Guid clienteId)
        {
            this.AggregateId = pedidoId;
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
    }
}