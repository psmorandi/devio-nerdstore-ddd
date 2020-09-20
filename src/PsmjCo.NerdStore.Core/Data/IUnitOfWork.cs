namespace PsmjCo.NerdStore.Core.Data
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}