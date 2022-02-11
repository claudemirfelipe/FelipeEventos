using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FelipeEventos.Application.Dtos;

namespace FelipeEventos.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount (UserUpdateDto userUpdateDto);
    }
}