﻿namespace PsmjCo.NerdStore.Vendas.Application.Queries.ViewModels
{
    using System;

    public class CarrinhoItemViewModel
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

    }
}