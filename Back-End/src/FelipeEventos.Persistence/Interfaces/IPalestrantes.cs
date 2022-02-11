using System.Threading.Tasks;
using FelipeEventos.Domain.Models;
using FelipeEventos.Persistence.Paginacao;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface IPalestrantes : IGeral
    {
         Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
         Task<Palestrante> GetPalestrantesByUserIdAsync(int userId, bool includeEventos = false);

    }
}