namespace PsmjCo.NerdStore.Catalogo.Domain
{
    using Core.DomainObjects;

    public class Dimensoes
    {
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(altura, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validacoes.ValidarSeMenorQue(largura, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validacoes.ValidarSeMenorQue(profundidade, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            this.Altura = altura;
            this.Largura = largura;
            this.Profundidade = profundidade;
        }

        public decimal Altura { get; }
        public decimal Largura { get; }
        public decimal Profundidade { get; }

        public string DescricaoFormatada()
        {
            return $"LxAxP: {this.Largura} x {this.Altura} x {this.Profundidade}";
        }

        public override string ToString()
        {
            return this.DescricaoFormatada();
        }
    }
}