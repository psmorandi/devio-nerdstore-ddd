namespace PsmjCo.NerdStore.Pagamentos.Business
{
    using System;
    using Core.DomainObjects;

    public class Pagamento : Entity, IAggregateRoot
    {
        public string CvvCartao { get; set; }
        public string ExpiracaoCartao { get; set; }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public Guid PedidoId { get; set; }
        public string Status { get; set; }

        // EF. Rel.
        public Transacao Transacao { get; set; }
        public decimal Valor { get; set; }
    }
}