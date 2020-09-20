namespace PsmjCo.NerdStore.WebApp.MVC.Extensions
{
    using System.Threading.Tasks;
    using Core.Messages.CommonMessages.Notifications;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class SummaryViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler notifications;

        public SummaryViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            this.notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(this.notifications.ObterNotificacoes());
            notificacoes.ForEach(c => this.ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return this.View();
        }
    }
}