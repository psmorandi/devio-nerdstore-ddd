namespace PsmjCo.NerdStore.Catalogo.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.DomainObjects;
    using Domain;
    using global::AutoMapper;
    using ViewModels;

    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IEstoqueService estoqueService;
        private readonly IMapper mapper;
        private readonly IProdutoRepository produtoRepository;

        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper, IEstoqueService estoqueService)
        {
            this.produtoRepository = produtoRepository;
            this.mapper = mapper;
            this.estoqueService = estoqueService;
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = this.mapper.Map<Produto>(produtoViewModel);
            this.produtoRepository.Adicionar(produto);

            await this.produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = this.mapper.Map<Produto>(produtoViewModel);
            this.produtoRepository.Atualizar(produto);

            await this.produtoRepository.UnitOfWork.Commit();
        }

        public async Task AdicionarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var categoria = this.mapper.Map<Categoria>(categoriaViewModel);
            
            this.produtoRepository.Adicionar(categoria);

            await this.produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var categoria = this.mapper.Map<Categoria>(categoriaViewModel);
            
            this.produtoRepository.Atualizar(categoria);

            await this.produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            if (!await this.estoqueService.DebitarEstoque(id, quantidade)) throw new DomainException("Falha ao debitar estoque");

            return this.mapper.Map<ProdutoViewModel>(await this.produtoRepository.ObterPorId(id));
        }

        public void Dispose()
        {
            this.produtoRepository?.Dispose();
            this.estoqueService?.Dispose();
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            return this.mapper.Map<IEnumerable<CategoriaViewModel>>(await this.produtoRepository.ObterCategorias());
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            return this.mapper.Map<IEnumerable<ProdutoViewModel>>(await this.produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            return this.mapper.Map<ProdutoViewModel>(await this.produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return this.mapper.Map<IEnumerable<ProdutoViewModel>>(await this.produtoRepository.ObterTodos());
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            if (!await this.estoqueService.ReporEstoque(id, quantidade)) throw new DomainException("Falha ao repor estoque");

            return this.mapper.Map<ProdutoViewModel>(await this.produtoRepository.ObterPorId(id));
        }
    }
}