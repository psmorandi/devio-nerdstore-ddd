namespace PsmjCo.NerdStore.Catalogo.Domain
{
    using System;
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.DomainObjects.DTO;
    using Core.Messages.CommonMessages.Notifications;
    using Events;

    public class EstoqueService : IEstoqueService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(
            IProdutoRepository produtoRepository,
            IMediatorHandler mediatorHandler)
        {
            this._produtoRepository = produtoRepository;
            this._mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await this.DebitarItemEstoque(produtoId, quantidade)) return false;

            return await this._produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
                if (!await this.DebitarItemEstoque(item.Id, item.Quantidade))
                    return false;

            return await this._produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            this._produtoRepository.Dispose();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await this.ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await this._produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens) await this.ReporItemEstoque(item.Id, item.Quantidade);

            return await this._produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await this._produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await this._mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10) await this._mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));

            this._produtoRepository.Atualizar(produto);
            return true;
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await this._produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            this._produtoRepository.Atualizar(produto);

            return true;
        }
    }
}