namespace PsmjCo.NerdStore.Catalogo.Domain
{
    using System.Collections.Generic;
    using Core.DomainObjects;

    public class Categoria : Entity
    {
        public Categoria(string nome, int codigo)
        {
            this.Nome = nome;
            this.Codigo = codigo;

            this.Validar();
        }

        protected Categoria() { }

        public int Codigo { get; }
        public string Nome { get; }

        // EF Relation
        public ICollection<Produto> Produtos { get; set; }

        public override string ToString()
        {
            return $"{this.Nome} - {this.Codigo}";
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(this.Nome, "O campo Nome da categoria não pode estar vazio");
            Validacoes.ValidarSeIgual(this.Codigo, 0, "O campo Codigo não pode ser 0");
        }
    }
}