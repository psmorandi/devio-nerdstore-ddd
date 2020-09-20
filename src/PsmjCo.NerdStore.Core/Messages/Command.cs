namespace PsmjCo.NerdStore.Core.Messages
{
    using System;
    using FluentValidation.Results;
    using MediatR;

    public abstract class Command : Message, IRequest<bool>
    {
        protected Command()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}