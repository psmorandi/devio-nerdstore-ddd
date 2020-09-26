namespace PsmjCo.NerdStore.Core.Communication.Mediator
{
    using System.Threading.Tasks;
    using Messages;
    using Messages.CommonMessages.Notifications;
    using PsmjCo.NerdStore.Core.Messages.CommonMessages.DomainEvents;

    public interface IMediatorHandler
    {
        Task<bool> EnviarComando<T>(T comando)
            where T : Command;

        Task PublicarEvento<T>(T evento)
            where T : Event;

        Task PublicarNotificacao<T>(T notificacao)
            where T : DomainNotification;

        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}