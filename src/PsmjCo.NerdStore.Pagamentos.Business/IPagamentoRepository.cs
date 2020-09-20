namespace PsmjCo.NerdStore.Pagamentos.Business
{
    using Core.Data;

    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void Adicionar(Pagamento pagamento);

        void AdicionarTransacao(Transacao transacao);
    }
}