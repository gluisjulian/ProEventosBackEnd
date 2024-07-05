using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Persistence.Data;


namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly ProEventosContext _context;

        public EventosController(ProEventosContext context)
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
            return _context.Eventos.Where(x => x.Id == id);
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
