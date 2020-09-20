using System;

namespace PsmjCo.NerdStore.Core.Messages
{
    using MediatR;

    public abstract class Event : Message, INotification
    {
        protected Event()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
    }
}