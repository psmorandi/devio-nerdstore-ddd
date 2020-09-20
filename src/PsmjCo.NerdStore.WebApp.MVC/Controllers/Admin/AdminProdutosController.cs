namespace PsmjCo.NerdStore.WebApp.MVC.Controllers.Admin
{
    using System;
    using System.Threading.Tasks;
    using Catalogo.Application.Services;
    using Catalogo.Application.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            this.produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return this.View("Estoque", await this.produtoAppService.ObterPorId(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
                await this.produtoAppService.ReporEstoque(id, quantidade);
            else
                await this.produtoAppService.DebitarEstoque(id, quantidade);

            return this.View("Index", await this.produtoAppService.ObterTodos());
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            return this.View(await this.PopularCategorias(await this.produtoAppService.ObterPorId(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await this.produtoAppService.ObterPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            this.ModelState.Remove("QuantidadeEstoque");
            if (!this.ModelState.IsValid) return this.View(await this.PopularCategorias(produtoViewModel));

            await this.produtoAppService.AtualizarProduto(produtoViewModel);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            return this.View(await this.produtoAppService.ObterTodos());
        }

        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return this.View(await this.PopularCategorias(new ProdutoViewModel()));
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!this.ModelState.IsValid) return this.View(await this.PopularCategorias(produtoViewModel));

            await this.produtoAppService.AdicionarProduto(produtoViewModel);

            return this.RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = await this.produtoAppService.ObterCategorias();
            return produto;
        }
    }
}