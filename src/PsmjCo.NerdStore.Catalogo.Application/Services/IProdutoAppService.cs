namespace PsmjCo.NerdStore.Catalogo.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IProdutoAppService : IDisposable
    {
        Task AdicionarProduto(ProdutoViewModel produtoViewModel);

        Task AtualizarProduto(ProdutoViewModel produtoViewModel);

        Task AdicionarCategoria(CategoriaViewModel categoriaViewModel);

        Task AtualizarCategoria(CategoriaViewModel categoriaViewModel);

        Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade);

        Task<IEnumerable<CategoriaViewModel>> ObterCategorias();

        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);

        Task<ProdutoViewModel> ObterPorId(Guid id);

        Task<IEnumerable<ProdutoViewModel>> ObterTodos();

        Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade);
    }
}