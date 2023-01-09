using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : GeralPersist, IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Palestrante> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                //.Include(p => p.User)
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            //query = query.AsNoTracking()
            //             .Where(p => (p.MiniCurriculo.ToLower().Contains(pageParams.Term.ToLower()) ||
            //                          p.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
            //                          p.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())) &&
            //                          p.User.Funcao == Domain.Enum.Funcao.Palestrante)
            //             .OrderBy(p => p.Id);

            //return await PageList<Palestrante>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);

            return null;
        }

        public async Task<Palestrante> GetPalestranteByUserIdAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                //.Include(p => p.User)
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);
                                        //.Where(p => p.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}