namespace PsmjCo.NerdStore.Core.Data.EventSourcing
{
    using System;

    public class StoredEvent
    {
        public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
        {
            this.Id = id;
            this.Tipo = tipo;
            this.DataOcorrencia = dataOcorrencia;
            this.Dados = dados;
        }

        public string Dados { get; }

        public DateTime DataOcorrencia { get; set; }

        public Guid Id { get; }

        public string Tipo { get; }
    }
}