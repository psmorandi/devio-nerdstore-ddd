namespace PsmjCo.NerdStore.Core.Data.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messages;

    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}