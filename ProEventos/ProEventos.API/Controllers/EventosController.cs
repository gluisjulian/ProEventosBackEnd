using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DataContext _context;

        public EventosController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Evento> GetAll()
        {
            var evento = _context.Eventos.ToList();
            return evento;
        }


        [HttpGet("{id}")]
        public IEnumerable<Evento> GetAll(int id)
        {
            return _context.Eventos.Where(x => x.EventoId == id);
        }

        [HttpPost]
        public Evento AddEvento(Evento evento)
        {
            if (!ModelState.IsValid)
                return null;

            _context.Add(evento);
            _context.SaveChanges();

            return evento;
        }
    }
}
