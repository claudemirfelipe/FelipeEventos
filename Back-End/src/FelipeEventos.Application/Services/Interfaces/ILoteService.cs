using System.Threading.Tasks;
using FelipeEventos.Application.Dtos;


namespace FelipeEventos.Application.Services.Interfaces
{
    public interface ILoteService
    {
         Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
         Task<bool> DeleteLote(int eventoId, int loteId);
         Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
         Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}