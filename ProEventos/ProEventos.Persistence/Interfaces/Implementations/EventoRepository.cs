using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Data;

namespace ProEventos.Persistence.Interfaces.Implementations
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ProEventosContext _context;

        public EventoRepository(ProEventosContext context)
        {
            _context = context;
        }


        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                       .Include(e => e.Lote)
                                       .Include(e => e.RedesSocials);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                       .Include(e => e.Lote)
                                       .Include(e => e.RedesSocials);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                       .Include(e => e.Lote)
                                       .Include(e => e.RedesSocials);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
    }
}
