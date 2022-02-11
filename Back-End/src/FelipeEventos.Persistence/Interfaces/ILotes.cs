using System.Threading.Tasks;
using FelipeEventos.Domain.Models;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface ILotes
    {
         Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
         Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}