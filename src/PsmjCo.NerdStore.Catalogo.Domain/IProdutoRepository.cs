namespace PsmjCo.NerdStore.Catalogo.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Data;

    public interface IProdutoRepository : IRepository<Produto>
    {
        void Adicionar(Produto produto);

        void Adicionar(Categoria categoria);

        void Atualizar(Produto produto);

        void Atualizar(Categoria categoria);

        Task<IEnumerable<Categoria>> ObterCategorias();

        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);

        Task<Produto> ObterPorId(Guid id);

        Task<IEnumerable<Produto>> ObterTodos();
    }
}