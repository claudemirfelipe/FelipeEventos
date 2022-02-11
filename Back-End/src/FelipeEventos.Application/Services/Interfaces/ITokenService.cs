using System.Threading.Tasks;
using FelipeEventos.Application.Dtos;

namespace FelipeEventos.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}