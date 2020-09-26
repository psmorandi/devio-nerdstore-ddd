namespace PsmjCo.NerdStore.Core.Communication.Mediator
{
    using System.Threading.Tasks;
    using Data.EventSourcing;
    using MediatR;
    using Messages;
    using Messages.CommonMessages.DomainEvents;
    using Messages.CommonMessages.Notifications;

    public class MediatorHandler : IMediatorHandler
    {
        private readonly IEventSourcingRepository eventSourcingRepository;
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            this.mediator = mediator;
            this.eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<bool> EnviarComando<T>(T comando)
            where T : Command
        {
            return await this.mediator.Send(comando);
        }

        public async Task PublicarDomainEvent<T>(T notificacao)
            where T : DomainEvent
        {
            await this.mediator.Publish(notificacao);
        }

        public async Task PublicarEvento<T>(T evento)
            where T : Event
        {
            await this.mediator.Publish(evento);
            await this.eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao)
            where T : DomainNotification
        {
            await this.mediator.Publish(notificacao);
        }
    }
}