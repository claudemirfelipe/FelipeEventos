using System.Threading.Tasks;
using FelipeEventos.Domain.Models;
using FelipeEventos.Persistence.Paginacao;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface IEventos
    {
         //Eventos
         Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes);
         Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes);
    }
}