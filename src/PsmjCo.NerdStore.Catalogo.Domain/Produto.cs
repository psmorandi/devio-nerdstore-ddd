namespace PsmjCo.NerdStore.Catalogo.Domain
{
    using System;
    using Core.DomainObjects;

    public class Produto : Entity, IAggregateRoot
    {
        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, DateTime dataCadastro, string imagem, Dimensoes dimensoes)
        {
            this.CategoriaId = categoriaId;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Ativo = ativo;
            this.Valor = valor;
            this.DataCadastro = dataCadastro;
            this.Imagem = imagem;
            this.Dimensoes = dimensoes;

            this.Validar();
        }

        protected Produto() { }

        public bool Ativo { get; private set; }
        public Categoria Categoria { get; private set; }
        public Guid CategoriaId { get; private set; }
        public DateTime DataCadastro { get; }
        public string Descricao { get; private set; }
        public Dimensoes Dimensoes { get; }
        public string Imagem { get; }
        public string Nome { get; }
        public int QuantidadeEstoque { get; private set; }
        public decimal Valor { get; }

        public void AlterarCategoria(Categoria categoria)
        {
            this.Categoria = categoria;
            this.CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
            Validacoes.ValidarSeVazio(descricao, "O campo Descricao do produto não pode estar vazio");
            this.Descricao = descricao;
        }

        public void Ativar()
        {
            this.Ativo = true;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!this.PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente");
            this.QuantidadeEstoque -= quantidade;
        }

        public void Desativar()
        {
            this.Ativo = false;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return this.QuantidadeEstoque >= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            this.QuantidadeEstoque += quantidade;
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(this.Nome, "O campo Nome do produto não pode estar vazio");
            Validacoes.ValidarSeVazio(this.Descricao, "O campo Descricao do produto não pode estar vazio");
            Validacoes.ValidarSeIgual(this.CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validacoes.ValidarSeMenorQue(this.Valor, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validacoes.ValidarSeVazio(this.Imagem, "O campo Imagem do produto não pode estar vazio");
        }
    }
}