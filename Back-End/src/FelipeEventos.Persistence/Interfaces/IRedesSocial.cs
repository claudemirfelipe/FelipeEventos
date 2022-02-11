using System.Threading.Tasks;
using FelipeEventos.Domain.Models;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface IRedesSocial : IGeral
    {
        Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id);
        Task<RedeSocial> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId);
        Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId);
    }
}