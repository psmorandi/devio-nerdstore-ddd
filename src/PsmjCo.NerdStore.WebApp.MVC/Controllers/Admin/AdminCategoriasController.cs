namespace PsmjCo.NerdStore.WebApp.MVC.Controllers.Admin
{
    using System.Threading.Tasks;
    using Catalogo.Application.Services;
    using Catalogo.Application.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AdminCategoriasController : Controller
    {
        private readonly IProdutoAppService produtoAppService;

        public AdminCategoriasController(IProdutoAppService produtoAppService)
        {
            this.produtoAppService = produtoAppService;
        }

        [Route("nova-categoria")]
        public IActionResult NovaCategoria()
        {
            return this.View(new CategoriaViewModel());
        }

        [Route("nova-categoria")]
        [HttpPost]
        public async Task<IActionResult> NovaCategoria(CategoriaViewModel categoriaViewModel)
        {
            if (!this.ModelState.IsValid) return this.View(categoriaViewModel);

            await this.produtoAppService.AdicionarCategoria(categoriaViewModel);

            return this.RedirectToAction("Index", "Vitrine");
        }
    }
}