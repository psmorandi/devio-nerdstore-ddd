namespace PsmjCo.NerdStore.Core.Messages
{
    using System;

    public abstract class Message
    {
        protected Message()
        {
            this.MessageType = this.GetType().Name;
        }

        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }
    }
}