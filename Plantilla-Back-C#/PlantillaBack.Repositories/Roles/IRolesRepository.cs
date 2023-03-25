using PlantillaBack.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Repositories.Roles
{
    public interface IRolesRepository
    {
        Task<Rol?> Get(int id);
    }
}
