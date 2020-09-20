namespace PsmjCo.NerdStore.Pagamentos.AntiCorruption
{
    using Business;

    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPayPalGateway payPalGateway;
        private readonly IConfigurationManager configManager;

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            this.payPalGateway = payPalGateway;
            this.configManager = configManager;
        }

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var apiKey = this.configManager.GetValue("apiKey");
            var encriptionKey = this.configManager.GetValue("encriptionKey");

            var serviceKey = this.payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = this.payPalGateway.GetCardHashKey(serviceKey, pagamento.NumeroCartao);

            var sucessoNoPagamento = this.payPalGateway.CommitTransaction(cardHashKey, pedido.Id.ToString(), pagamento.Valor);

            // TODO: O gateway de pagamentos que deve retornar o objeto transação
            var transacao = new Transacao
                            {
                                PedidoId = pedido.Id,
                                Total = pedido.Valor,
                                PagamentoId = pagamento.Id
                            };
            
            transacao.StatusTransacao = sucessoNoPagamento ? StatusTransacao.Pago : StatusTransacao.Recusado;
            return transacao;
        }
    }
}