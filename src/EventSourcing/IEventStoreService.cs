namespace EventSourcing
{
    using EventStore.ClientAPI;

    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}