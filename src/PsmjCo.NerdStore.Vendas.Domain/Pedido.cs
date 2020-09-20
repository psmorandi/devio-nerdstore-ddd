namespace PsmjCo.NerdStore.Vendas.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.DomainObjects;
    using FluentValidation.Results;

    public class Pedido : Entity, IAggregateRoot
    {
        private readonly List<PedidoItem> pedidoItems;

        public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
        {
            this.ClienteId = clienteId;
            this.VoucherUtilizado = voucherUtilizado;
            this.Desconto = desconto;
            this.ValorTotal = valorTotal;
            this.pedidoItems = new List<PedidoItem>();
        }

        protected Pedido()
        {
            this.pedidoItems = new List<PedidoItem>();
        }

        public Guid ClienteId { get; private set; }
        public int Codigo { get; }
        public DateTime DataCadastro { get; }
        public decimal Desconto { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => this.pedidoItems;
        public PedidoStatus PedidoStatus { get; private set; }

        public decimal ValorTotal { get; private set; }

        // EF relation
        public Voucher Voucher { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }

        public void AdicionarItem(PedidoItem item)
        {
            if (!item.EhValido()) return;

            item.AssociarPedido(this.Id);

            if (this.PedidoItemExistente(item))
            {
                var itemExistente = this.pedidoItems.First(p => p.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                this.pedidoItems.Remove(itemExistente);
            }
            this.pedidoItems.Add(item);

            this.CalcularValorPedido();
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var validationResult = voucher.VerficarSeAplicavel();
            if (!validationResult.IsValid) return validationResult;

            this.Voucher = voucher;
            this.VoucherUtilizado = true;
            this.CalcularValorPedido();

            return validationResult;
        }

        public void AtualizarItem(PedidoItem item)
        {
            if (!item.EhValido()) return;
            item.AssociarPedido(this.Id);

            var itemExistente = this.PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido.");
            this.pedidoItems.Remove(itemExistente);
            this.pedidoItems.Add(item);

            this.CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            this.AtualizarItem(item);
        }

        public void CalcularValorPedido()
        {
            this.ValorTotal = this.PedidoItems.Sum(p => p.CalcularValor());
            this.CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!this.VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = this.ValorTotal;

            if (this.Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcetagem)
            {
                desconto = valor * this.Voucher.Percentual.Value / 100;
                valor -= desconto;
            }
            else
            {
                if (this.Voucher.ValorDesconto.HasValue)
                {
                    desconto = this.Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            this.ValorTotal = valor < 0 ? 0 : valor;
            this.Desconto = desconto;
        }

        public void CancelarRascunho()
        {
            this.PedidoStatus = PedidoStatus.Cancelado;
        }

        public void FinalizarRascunho()
        {
            this.PedidoStatus = PedidoStatus.Pago;
        }

        public void IniciarPedido()
        {
            this.PedidoStatus = PedidoStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            this.PedidoStatus = PedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            this.PedidoStatus = PedidoStatus.Cancelado;
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return this.pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void RemoverItem(PedidoItem item)
        {
            if (!item.EhValido()) return;

            var itemExistente = this.PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido.");
            this.pedidoItems.Remove(itemExistente);

            this.CalcularValorPedido();
        }

        public void TornarRascunho()
        {
            this.PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clientId)
            {
                var pedido = new Pedido
                             {
                                 ClienteId = clientId
                             };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}