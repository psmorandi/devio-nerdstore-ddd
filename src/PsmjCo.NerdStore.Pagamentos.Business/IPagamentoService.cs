namespace PsmjCo.NerdStore.Pagamentos.Business
{
    using System.Threading.Tasks;
    using Core.DomainObjects.DTO;

    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}