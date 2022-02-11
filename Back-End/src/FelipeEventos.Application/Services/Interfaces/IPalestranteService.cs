using System.Threading.Tasks;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Persistence.Paginacao;

namespace FelipeEventos.Application.Services.Interfaces
{
    public interface IPalestranteService
    {
         Task<PalestranteDto> AddPalestrantes(int userId, PalestranteAddDto model);
         Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model);
         Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
         Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}


        
