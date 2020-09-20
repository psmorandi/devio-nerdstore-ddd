namespace PsmjCo.NerdStore.Vendas.Application.Events
{
    using System;
    using Core.Messages;

    public class VoucherAplicadoPedidoEvent : Event
    {
        public VoucherAplicadoPedidoEvent(Guid clienteId, Guid pedidoId, Guid voucherId)
        {
            this.ClienteId = clienteId;
            this.PedidoId = pedidoId;
            this.VoucherId = voucherId;
        }

        public Guid ClienteId { get; }
        public Guid PedidoId { get; }
        public Guid VoucherId { get; }
    }
}