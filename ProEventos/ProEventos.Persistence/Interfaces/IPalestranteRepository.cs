using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IPalestranteRepository
    {
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos);
    }
}
