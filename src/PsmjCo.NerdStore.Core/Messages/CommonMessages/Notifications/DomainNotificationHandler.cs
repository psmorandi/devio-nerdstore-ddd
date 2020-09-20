namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> notifications;

        public DomainNotificationHandler()
        {
            this.notifications = new List<DomainNotification>();
        }

        public void Dispose()
        {
            this.notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            this.notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> ObterNotificacoes()
        {
            return this.notifications;
        }

        public virtual bool TemNotificacao()
        {
            return this.ObterNotificacoes().Any();
        }
    }
}