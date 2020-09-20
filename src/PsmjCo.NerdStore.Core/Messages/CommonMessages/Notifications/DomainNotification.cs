namespace PsmjCo.NerdStore.Core.Messages.CommonMessages.Notifications
{
    using System;
    using MediatR;

    public class DomainNotification : Message, INotification
    {
        public DomainNotification(string key, string value)
        {
            this.Timestamp = DateTime.UtcNow;
            this.DomainNotificationId = Guid.NewGuid();
            this.Version = 1;
            this.Key = key;
            this.Value = value;
        }

        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public DateTime Timestamp { get; }
        public string Value { get; }
        public int Version { get; }
    }
}