﻿namespace PsmjCo.NerdStore.Vendas.Application.Queries.ViewModels
{
    using System;

    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoStatus { get; set; }
    }
}