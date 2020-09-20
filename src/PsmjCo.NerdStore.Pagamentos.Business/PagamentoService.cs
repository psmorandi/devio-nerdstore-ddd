namespace PsmjCo.NerdStore.Pagamentos.Business
{
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.DomainObjects.DTO;
    using Core.Messages.CommonMessages.IntegrationEvents;
    using Core.Messages.CommonMessages.Notifications;

    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository pagamentoRepository;
        private readonly IMediatorHandler mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IPagamentoRepository pagamentoRepository, 
                                IMediatorHandler mediatorHandler)
        {
            this.pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            this.pagamentoRepository = pagamentoRepository;
            this.mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId,
                Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                Valor = pagamentoPedido.Total,
                NomeCartao = pagamentoPedido.NomeCartao,
                NumeroCartao = pagamentoPedido.NumeroCartao,
                ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                CvvCartao = pagamentoPedido.CvvCartao,
                PedidoId = pagamentoPedido.PedidoId
            };

            var transacao = this.pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            pagamento.Status = transacao.StatusTransacao.ToString();

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

                this.pagamentoRepository.Adicionar(pagamento);
                this.pagamentoRepository.AdicionarTransacao(transacao);

                await this.pagamentoRepository.UnitOfWork.Commit();
                return transacao;
            }

            await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento","A operadora recusou o pagamento"));
            await this.mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;
        }
    }
}