using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Application.Services.Interfaces;
using FelipeEventos.Domain.Identity;
using FelipeEventos.Persistence.Interfaces;

namespace FelipeEventos.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUsers _users;
        public UserService(UserManager<User> userManager, 
                           SignInManager<User> signInManager, 
                           IMapper mapper, 
                           IUsers users)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _users = users;
            
        }
        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao tentar verificar verificar se o Usuário existe. Erro: {ex.Message}");
            };
        }
        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _users.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao tentar pegar o Usuário pelo Username. Erro: {ex.Message}");
            };
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao tentar verificar o password. Erro: {ex.Message}");
            };
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }

                return null;

            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao tentar criar o Usuário. Erro: {ex.Message}");
            };
        }
        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _users.GetUserByUserNameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                userUpdateDto.Id = user.Id;

                _mapper.Map(userUpdateDto, user);

                if (userUpdateDto.Password != null) 
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }
                

                _users.Update<User>(user);
                
                if (await _users.SaveChangesAsync())
                {
                    var userRetorno = await _users.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }
                return null; 
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao tantar atualizar o Usuário. Erro: {ex.Message}");
            };
        }
    }
}