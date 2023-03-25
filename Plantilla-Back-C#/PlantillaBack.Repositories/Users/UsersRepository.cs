using Microsoft.EntityFrameworkCore;
using PlantillaBack.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        readonly DataBaseContext _context;

        public UsersRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).Include(x => x.Rol).FirstOrDefaultAsync();

        }

        public async Task<User?> Get(string email, string password)
        {
            return await(from user in _context.Users
                         join rol in _context.Roles on user.RolId equals rol.Id
                         where user.Email.ToLower() == email.ToLower() && user.Password == password
                         select new User
                         {
                             Email = user.Email,
                             Id = user.Id,
                             RolId = rol.Id,
                             Rol = rol,
                             Name = user.Name
                         }).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateUserRole(int id, int rolId)
        {
            return await _context.Users
                     .Where(x => x.Id == id)
                     .ExecuteUpdateAsync(Users => Users.SetProperty
                     (property => property.RolId,
                     property => rolId));
        }
    }
}
