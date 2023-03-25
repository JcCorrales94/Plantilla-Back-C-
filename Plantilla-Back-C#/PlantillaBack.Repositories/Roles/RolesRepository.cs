using Microsoft.EntityFrameworkCore;
using PlantillaBack.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Repositories.Roles
{
    public class RolesRepository : IRolesRepository
    {
        readonly DataBaseContext _context;

        public RolesRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Rol?> Get(int id)
        {
            return await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
