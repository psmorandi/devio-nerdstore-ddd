namespace PsmjCo.NerdStore.Core.DomainObjects.DTO
{
    using System;
    using System.Collections.Generic;

    public class ListaProdutosPedido
    {
        public ICollection<Item> Itens { get; set; }
        public Guid PedidoId { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}