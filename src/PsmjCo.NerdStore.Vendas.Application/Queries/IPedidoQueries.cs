namespace PsmjCo.NerdStore.Vendas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IPedidoQueries
    {
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);

        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
    }
}