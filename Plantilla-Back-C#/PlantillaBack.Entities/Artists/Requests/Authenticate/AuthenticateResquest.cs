using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Entities.Artists.Requests.Authenticate
{
    public class AuthenticateResquest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
