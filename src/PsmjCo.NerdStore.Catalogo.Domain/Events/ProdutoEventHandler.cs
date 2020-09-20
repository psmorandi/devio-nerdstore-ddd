namespace PsmjCo.NerdStore.Catalogo.Domain.Events
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.Messages.CommonMessages.IntegrationEvents;
    using MediatR;

    public class ProdutoEventHandler :
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IEstoqueService estoqueService;
        private readonly IMediatorHandler mediatorHandler;
        private readonly IProdutoRepository produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository, IEstoqueService estoqueService, IMediatorHandler mediatorHandler)
        {
            this.produtoRepository = produtoRepository;
            this.estoqueService = estoqueService;
            this.mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await this.produtoRepository.ObterPorId(mensagem.AggregateId);

            //enviar email
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            var result = await this.estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

            if (result)
                await this.mediatorHandler.PublicarEvento(
                    new PedidoEstoqueConfirmadoEvent(
                        message.PedidoId,
                        message.ClienteId,
                        message.Total,
                        message.ProdutosPedido,
                        message.NomeCartao,
                        message.NumeroCartao,
                        message.ExpiracaoCartao,
                        message.CvvCartao));
            else
                await this.mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
        {
            await this.estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);
        }
    }
}