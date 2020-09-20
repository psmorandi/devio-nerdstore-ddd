namespace PsmjCo.NerdStore.Pagamentos.Data.Repository
{
    using Business;
    using Core.Data;

    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => this._context;


        public void Adicionar(Pagamento pagamento)
        {
            this._context.Pagamentos.Add(pagamento);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            this._context.Transacoes.Add(transacao);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}