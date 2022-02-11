using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FelipeEventos.Domain.Identity;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface IUsers : IGeral
    {
         Task<IEnumerable<User>> GetUsersAsync();
         Task <User> GetUserByIdAsync(int id);
         Task <User> GetUserByUserNameAsync(string userName);
    }
}