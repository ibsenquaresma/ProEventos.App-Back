using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrantes(Palestrante model);
        Task<Palestrante> UpdatePalestrante(Palestrante model);

        Task<Palestrante> GetAllPalestrantesAsync( bool includeEventos = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}