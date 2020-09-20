namespace PsmjCo.NerdStore.WebApp.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Catalogo.Application.Services;
    using Core.Communication.Mediator;
    using Core.Messages.CommonMessages.Notifications;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Vendas.Application.Commands;
    using Vendas.Application.Queries;
    using Vendas.Application.Queries.ViewModels;

    public class CarrinhoController : ControllerBase
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IPedidoQueries pedidoQueries;
        private readonly IProdutoAppService produtoAppService;

        public CarrinhoController(
            INotificationHandler<DomainNotification> notifications,
            IProdutoAppService produtoAppService,
            IMediatorHandler mediatorHandler,
            IPedidoQueries pedidoQueries)
            : base(notifications, mediatorHandler)
        {
            this.produtoAppService = produtoAppService;
            this.mediatorHandler = mediatorHandler;
            this.pedidoQueries = pedidoQueries;
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await this.produtoAppService.ObterPorId(id);
            if (produto == null) return this.BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                this.TempData["Erro"] = "Produto com estoque insuficiente";
                return this.RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(this.ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await this.mediatorHandler.EnviarComando(command);

            if (this.OperacaoValida()) return this.RedirectToAction("Index");

            this.TempData["Erros"] = this.ObterMensagensErro();
            return this.RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(this.ClienteId, voucherCodigo);
            await this.mediatorHandler.EnviarComando(command);

            if (this.OperacaoValida()) return this.RedirectToAction("Index");

            return this.View("Index", await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await this.produtoAppService.ObterPorId(id);
            if (produto == null) return this.BadRequest();

            var command = new AtualizarItemPedidoCommand(this.ClienteId, id, quantidade);
            await this.mediatorHandler.EnviarComando(command);

            if (this.OperacaoValida()) return this.RedirectToAction("Index");

            return this.View("Index", await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return this.View(await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var carrinho = await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId);

            var command = new IniciarPedidoCommand(
                carrinho.PedidoId,
                this.ClienteId,
                carrinho.ValorTotal,
                carrinhoViewModel.Pagamento.NomeCartao,
                carrinhoViewModel.Pagamento.NumeroCartao,
                carrinhoViewModel.Pagamento.ExpiracaoCartao,
                carrinhoViewModel.Pagamento.CvvCartao);

            await this.mediatorHandler.EnviarComando(command);

            if (this.OperacaoValida()) return this.RedirectToAction("Index", "Pedido");

            return this.View("ResumoDaCompra", await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await this.produtoAppService.ObterPorId(id);
            if (produto == null) return this.BadRequest();

            var command = new RemoverItemPedidoCommand(this.ClienteId, id);
            await this.mediatorHandler.EnviarComando(command);

            if (this.OperacaoValida()) return this.RedirectToAction("Index");

            return this.View("Index", await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return this.View(await this.pedidoQueries.ObterCarrinhoCliente(this.ClienteId));
        }
    }
}