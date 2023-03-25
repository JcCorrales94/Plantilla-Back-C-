using PlantillaBack.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<User?> Get(string email);
        Task Create(User user);
        Task<User?> Get(string email, string password);
        Task<int> UpdateUserRole(int id, int rolId);
    }
}
