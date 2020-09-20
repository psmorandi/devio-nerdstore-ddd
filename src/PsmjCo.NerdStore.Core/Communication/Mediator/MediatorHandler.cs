namespace PsmjCo.NerdStore.Core.Communication.Mediator
{
    using System.Threading.Tasks;
    using MediatR;
    using Messages;
    using Messages.CommonMessages.Notifications;

    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando)
            where T : Command
        {
            return await this.mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento)
            where T : Event
        {
            await this.mediator.Publish(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao)
            where T : DomainNotification
        {
            await this.mediator.Publish(notificacao);
        }
    }
}