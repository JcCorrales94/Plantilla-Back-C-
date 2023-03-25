using PlantillaBack.Entities.Artists.Requests.Authenticate;
using PlantillaBack.Entities.Users.Requests.Create;
using PlantillaBack.Entities.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Servicies.Users
{
    public interface IUsersServices
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateResquest authenticateResquest);

        Task Register(RegisterUserRequest registerUserRequest);

        Task UpdateUserRole(int id, int roleId);
    }
}
