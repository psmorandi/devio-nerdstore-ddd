namespace PsmjCo.NerdStore.Core.Data
{
    using System;
    using DomainObjects;

    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}