namespace EventSourcing
{
    using System;
    using EventStore.ClientAPI;
    using Microsoft.Extensions.Configuration;

    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection connection;

        public EventStoreService(IConfiguration configuration)
        {
            this.connection = EventStoreConnection.Create(
                configuration.GetConnectionString("EventStoreConnection"));

            this.connection.ConnectAsync().Wait();
        }

        public IEventStoreConnection GetConnection()
        {
            return this.connection;
        }
    }
}