namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.DomainEvents
{
    using System;
    using Messages;

    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }
    }
}