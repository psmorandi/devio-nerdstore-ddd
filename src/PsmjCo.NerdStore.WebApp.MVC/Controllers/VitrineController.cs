namespace PsmjCo.NerdStore.WebApp.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Catalogo.Application.Services;
    using Microsoft.AspNetCore.Mvc;

    public class VitrineController : Controller
    {
        private readonly IProdutoAppService produtoAppService;

        public VitrineController(IProdutoAppService produtoAppService)
        {
            this.produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            return View(await this.produtoAppService.ObterTodos());
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View(await this.produtoAppService.ObterPorId(id));
        }
    }
}