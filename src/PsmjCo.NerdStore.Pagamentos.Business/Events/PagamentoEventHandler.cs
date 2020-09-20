namespace PsmjCo.NerdStore.Pagamentos.Business.Events
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.DomainObjects.DTO;
    using Core.Messages.CommonMessages.IntegrationEvents;
    using MediatR;

    public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
    {
        private readonly IPagamentoService pagamentoService;

        public PagamentoEventHandler(IPagamentoService pagamentoService)
        {
            this.pagamentoService = pagamentoService;
        }

        public async Task Handle(PedidoEstoqueConfirmadoEvent message, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PagamentoPedido
                                  {
                                      PedidoId = message.PedidoId,
                                      ClienteId = message.ClienteId,
                                      Total = message.Total,
                                      NomeCartao = message.NomeCartao,
                                      NumeroCartao = message.NumeroCartao,
                                      ExpiracaoCartao = message.ExpiracaoCartao,
                                      CvvCartao = message.CvvCartao
                                  };

            await this.pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
        }
    }
}