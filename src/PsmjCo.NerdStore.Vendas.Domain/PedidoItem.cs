namespace PsmjCo.NerdStore.Vendas.Domain
{
    using System;
    using Core.DomainObjects;

    public class PedidoItem : Entity
    {
        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            this.ProdutoId = produtoId;
            this.ProdutoNome = produtoNome;
            this.Quantidade = quantidade;
            this.ValorUnitario = valorUnitario;
        }

        //EF Relation
        protected PedidoItem() { }

        //EF Relation
        public Pedido Pedido { get; set; }

        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; }
        public string ProdutoNome { get; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; }

        public decimal CalcularValor()
        {
            return this.Quantidade * this.ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            this.Quantidade += unidades;
        }

        internal void AssociarPedido(Guid pedidoItem)
        {
            this.PedidoId = pedidoItem;
        }

        internal void AtualizarUnidades(int unidades)
        {
            this.Quantidade = unidades;
        }

        public override bool EhValido()
        {
            return true;
        }
    }
}