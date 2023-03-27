using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantillaBack.Entities.Artists.Requests.Authenticate;
using PlantillaBack.Entities.Users.Requests.Create;
using PlantillaBack.Servicies.Users;

namespace Plantilla_Back_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUsersServices _userServices;
        readonly ILogger<UsersController> _logger;

        public UsersController(IUsersServices userServices, ILogger<UsersController> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
        {
            await _userServices.Register(registerUserRequest);
            return Created("", null);
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateResquest authenticateRequest)
        {
            _logger.LogInformation($"Intento de login con usuario: {authenticateRequest.Email}");
            var authResponse = await _userServices.Authenticate(authenticateRequest);

            return Ok(authResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update/{id}/role/{rolId}")]
        public async Task<IActionResult> UpdateUserRole(int id, int rolId)
        {
            await _userServices.UpdateUserRole(id, rolId);
            return Ok();
        }

        [HttpGet]
        [Route("recover/password/{id}")]
        public async Task<IActionResult> ChangePassword(Guid id)
        {
            //? IR A BD A UNA TABLA EN LA QUE TENGAS EL GUID-EMAIL-VALIDEZ Y SI ES CORRECTO PERMITIR QUE ACTUALICE LA PASSWORD
            return Ok();
        }
    }
}
