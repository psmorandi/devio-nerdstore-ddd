namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.DomainEvents
{
    using System;
    using MediatR;

    public abstract class DomainEvent : Message, INotification
    {
        protected DomainEvent(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
            this.Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
    }
}