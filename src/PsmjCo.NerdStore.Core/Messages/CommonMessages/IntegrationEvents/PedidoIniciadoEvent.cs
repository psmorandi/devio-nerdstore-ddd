namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    using System;
    using DomainObjects.DTO;

    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public PedidoIniciadoEvent(
            Guid pedidoId,
            Guid clienteId,
            ListaProdutosPedido itens,
            decimal total,
            string nomeCartao,
            string numeroCartao,
            string expiracaoCartao,
            string cvvCartao)
        {
            this.AggregateId = pedidoId;
            this.PedidoId = pedidoId;
            this.ClienteId = clienteId;
            this.ProdutosPedido = itens;
            this.Total = total;
            this.NomeCartao = nomeCartao;
            this.NumeroCartao = numeroCartao;
            this.ExpiracaoCartao = expiracaoCartao;
            this.CvvCartao = cvvCartao;
        }

        public Guid ClienteId { get; }
        public string CvvCartao { get; }
        public string ExpiracaoCartao { get; }
        public string NomeCartao { get; }
        public string NumeroCartao { get; }
        public Guid PedidoId { get; }
        public ListaProdutosPedido ProdutosPedido { get; }
        public decimal Total { get; }
    }
}