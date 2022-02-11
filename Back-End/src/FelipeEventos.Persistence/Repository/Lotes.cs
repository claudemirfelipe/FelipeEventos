using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FelipeEventos.Domain.Models;
using FelipeEventos.Persistence.Data;
using FelipeEventos.Persistence.Interfaces;

namespace FelipeEventos.Persistence.Repository
{
    public class Lotes : ILotes
    {
        private readonly FelipeEventosContext _context;

        public Lotes(FelipeEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId && lote.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}