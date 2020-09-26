namespace PsmjCo.NerdStore.WebApp.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Data.EventSourcing;
    using Microsoft.AspNetCore.Mvc;

    public class EventosController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventosController(IEventSourcingRepository eventSourcingRepository)
        {
            this._eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("eventos/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await this._eventSourcingRepository.ObterEventos(id);
            return this.View(eventos);
        }
    }
}