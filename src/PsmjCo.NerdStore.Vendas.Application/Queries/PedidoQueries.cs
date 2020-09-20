namespace PsmjCo.NerdStore.Vendas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using ViewModels;

    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido == null) return null;

            var carrinho = new CarrinhoViewModel
                           {
                               ClienteId = pedido.ClienteId,
                               ValorTotal = pedido.ValorTotal,
                               PedidoId = pedido.Id,
                               ValorDesconto = pedido.Desconto,
                               SubTotal = pedido.Desconto + pedido.ValorTotal
                           };

            if (pedido.VoucherId != null) carrinho.VoucherCodigo = pedido.Voucher.Codigo;

            foreach (var item in pedido.PedidoItems)
                carrinho.Items.Add(
                    new CarrinhoItemViewModel
                    {
                        ProdutoId = item.ProdutoId,
                        ProdutoNome = item.ProdutoNome,
                        Quantidade = item.Quantidade,
                        ValorUnitario = item.ValorUnitario,
                        ValorTotal = item.ValorUnitario * item.Quantidade
                    });

            return carrinho;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = await this.pedidoRepository.ObterListaPorClienteId(clienteId);

            pedidos = pedidos.Where(p => p.PedidoStatus == PedidoStatus.Pago || p.PedidoStatus == PedidoStatus.Cancelado)
                .OrderByDescending(p => p.Codigo);

            var todosPedidos = pedidos.ToList();
            if (!todosPedidos.Any()) return null;

            return todosPedidos.Select(
                    pedido => new PedidoViewModel
                              {
                                  ValorTotal = pedido.ValorTotal, 
                                  PedidoStatus = (int)pedido.PedidoStatus, 
                                  Codigo = pedido.Codigo, 
                                  DataCadastro = pedido.DataCadastro
                              })
                .ToList();
        }
    }
}