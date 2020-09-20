namespace PsmjCo.NerdStore.Vendas.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Data;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext context;

        public PedidoRepository(VendasContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => this.context;

        public void Adicionar(Pedido pedido)
        {
            this.context.Pedidos.Add(pedido);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            this.context.PedidoItems.Add(pedidoItem);
        }

        public void Atualizar(Pedido pedido)
        {
            this.context.Pedidos.Update(pedido);
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            this.context.PedidoItems.Update(pedidoItem);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<PedidoItem> ObterItemPorId(Guid id)
        {
            return await this.context.PedidoItems.FindAsync(id);
        }

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await this.context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            return await this.context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            var pedido = await this.context.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
            if (pedido == null) return null;

            await this.context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            if (pedido.VoucherId != null)
                await this.context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();

            return pedido;
        }

        public async Task<Pedido> ObterPorId(Guid id)
        {
            return await this.context.Pedidos.FindAsync(id);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await this.context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            this.context.PedidoItems.Remove(pedidoItem);
        }
    }
}