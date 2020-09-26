namespace PsmjCo.NerdStore.WebApp.MVC.Controllers
{
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.Messages.CommonMessages.Notifications;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Vendas.Application.Queries;

    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(
            IPedidoQueries pedidoQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler)
        {
            this._pedidoQueries = pedidoQueries;
        }

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            return View(await this._pedidoQueries.ObterPedidosCliente(this.ClienteId));
        }
    }
}