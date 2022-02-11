using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FelipeEventos.Domain.Identity;
using FelipeEventos.Persistence.Data;
using FelipeEventos.Persistence.Interfaces;

namespace FelipeEventos.Persistence.Repository
{
    public class Users : Geral, IUsers
    {
        private readonly FelipeEventosContext _context;
        public Users(FelipeEventosContext context) : base(context)
        {
            _context = context;
            
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}