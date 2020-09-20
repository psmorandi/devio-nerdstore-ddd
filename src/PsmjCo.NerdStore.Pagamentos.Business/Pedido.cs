namespace PsmjCo.NerdStore.Pagamentos.Business
{
    using System;
    using System.Collections.Generic;

    public class Pedido
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}