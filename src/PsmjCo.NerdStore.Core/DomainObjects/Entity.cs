namespace PsmjCo.NerdStore.Core.DomainObjects
{
    using System;
    using System.Collections.Generic;
    using Messages;

    public abstract class Entity
    {
        private List<Event> notificacoes;

        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public IReadOnlyCollection<Event> Notificacoes => this.notificacoes?.AsReadOnly();

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public void AdicionarEvento(Event evento)
        {
            this.notificacoes ??= new List<Event>();
            this.notificacoes.Add(evento);
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            return compareTo is not null && this.Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() * 907 + this.Id.GetHashCode();
        }

        public void LimparEventos()
        {
            this.notificacoes?.Clear();
        }

        public void RemoverEvento(Event eventItem)
        {
            this.notificacoes?.Remove(eventItem);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} [Id={this.Id}]";
        }
    }
}