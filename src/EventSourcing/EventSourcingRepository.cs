namespace EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventStore.ClientAPI;
    using Newtonsoft.Json;
    using PsmjCo.NerdStore.Core.Data.EventSourcing;
    using PsmjCo.NerdStore.Core.Messages;

    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            this.eventStoreService = eventStoreService;
        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
        {
            var eventos = await eventStoreService.GetConnection()
                              .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

            var listaEventos = new List<StoredEvent>();

            foreach (var resolvedEvent in eventos.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<Event>(dataEncoded);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listaEventos.Add(evento);
            }

            return listaEventos.OrderBy(e => e.DataOcorrencia);

        }

        public async Task SalvarEvento<TEvent>(TEvent evento)
            where TEvent : Event
        {
            var connection = this.eventStoreService.GetConnection();

            var aggregateId = evento.AggregateId.ToString();

            var events = FormatarEvento(evento);

            await connection.AppendToStreamAsync(
                aggregateId,
                ExpectedVersion.Any,
                events);
        }

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento)
            where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                evento.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                null);
        }
    }
}