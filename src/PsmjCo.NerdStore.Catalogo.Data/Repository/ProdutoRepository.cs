namespace PsmjCo.NerdStore.Catalogo.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Data;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext context;

        public ProdutoRepository(CatalogoContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => this.context;

        public void Adicionar(Produto produto)
        {
            this.context.Produtos.Add(produto);
        }

        public void Adicionar(Categoria categoria)
        {
            this.context.Categorias.Add(categoria);
        }

        public void Atualizar(Produto produto)
        {
            this.context.Produtos.Update(produto);
        }

        public void Atualizar(Categoria categoria)
        {
            this.context.Categorias.Update(categoria);
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await this.context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            return await this.context.Produtos.AsNoTracking().Include(p => p.Categoria).Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await this.context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await this.context.Produtos.AsNoTracking().ToListAsync();
        }
    }
}