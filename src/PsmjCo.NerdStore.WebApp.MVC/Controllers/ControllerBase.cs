namespace PsmjCo.NerdStore.WebApp.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.Messages.CommonMessages.Notifications;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ControllerBase : Controller
    {
        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");
        private readonly IMediatorHandler mediatorHandler;
        private readonly DomainNotificationHandler notificationHandler;

        protected ControllerBase(INotificationHandler<DomainNotification> notificationHandler, IMediatorHandler mediatorHandler)
        {
            this.notificationHandler = (DomainNotificationHandler)notificationHandler;
            this.mediatorHandler = mediatorHandler;
        }

        protected async Task NotifcarErro(string codigo, string mensagem)
        {
            await this.mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return this.notificationHandler.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected bool OperacaoValida()
        {
            return !this.notificationHandler.TemNotificacao();
        }
    }
}