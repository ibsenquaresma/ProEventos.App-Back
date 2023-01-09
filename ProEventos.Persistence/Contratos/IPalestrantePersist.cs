using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist : IGeralPersist
    {
        Task<Palestrante> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(bool includeEventos = false);
    }
}