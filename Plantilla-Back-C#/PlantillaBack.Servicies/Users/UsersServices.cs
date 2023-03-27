using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlantillaBack.Entities.Artists.Requests.Authenticate;
using PlantillaBack.Entities.Users.Requests.Create;
using PlantillaBack.Repositories.Roles;
using PlantillaBack.Repositories.Users;
using PlantillaBack.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PlantillaBack.Entities.Users.Responses;
using PlantillaBack.Entities.Users;
using PlantillaBack.Services.Email;

namespace PlantillaBack.Servicies.Users
{
    public class UsersServices : IUsersServices
    {
        readonly IUsersRepository _usersRepository;
        readonly IConfiguration _configuration;
        readonly IRolesRepository _rolesRepository;
        readonly IEncryptionServices _encryptionServices;
        readonly IEmailService _emailService;

        public UsersServices(IUsersRepository usersRepository, IConfiguration configuration, IRolesRepository rolesRepository, IEncryptionServices encryptionServices, IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _rolesRepository = rolesRepository;
            _encryptionServices = encryptionServices;
            _emailService = emailService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateResquest authenticateResquest)
        {
            //? 1.- COMPROBAMOS QUE ESE USUARIO EXISTE EN NUESTRA BASE DE DATOS POR EL EMAIL

            var user = await _usersRepository.Get(authenticateResquest.Email);

            if (user == null)
            {
                throw new InvalidDataException("No se ha encontrado usuario con las credenciales indicadas (1)");
            }

            //? EN CASO DE QUE EXISTA:
            //? Desencriptamos la contraseña almacenada correspondiente al Email recuperado y se comprueba que coincida con la contraseña enviada por el usuario

            var descrytedPassword = _encryptionServices.DecryptString(user.Password);
            if (descrytedPassword != authenticateResquest.Password)
            {
                throw new InvalidDataException("No se ha encontrado usuario con las credenciales indicadas (2)");
            }

            //? SI EMAIL Y CONTRASEÑA COINCIDE SEGUIMOS POR AQUÍ,
            //? CREAMOS EL TOKEN CON LA INFORMACIÓN QUE QUEREMOS QUE CONTENGA DEL USUARIO   

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Rol.Description),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["JWT:ValidAudience"],
                Issuer = _configuration["JWT:ValidIssuer"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var authenticateResponse = new AuthenticateResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = tokenHandler.WriteToken(token),
            };
            return authenticateResponse;
        }

        public async Task Register(RegisterUserRequest registerUserRequest)
        {
            var validator = new RegisterUserRequestValidator();
            var result = validator.Validate(registerUserRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var userExistas = await _usersRepository.Get(registerUserRequest.Email);
            if (userExistas != null)
            {
                throw new InvalidDataException($"Ya existe un usuario con el email {registerUserRequest.Email}");
            }
            var newUser = new User
            {
                Email = registerUserRequest.Email,
                Name = registerUserRequest.Name,
                Password = _encryptionServices.EncryptString(registerUserRequest.Password),
                RolId = 2
            };
            await _usersRepository.Create(newUser);

            //MÉTODO PARA EL ENVIO DE EMAIL PARA EL RESGISTRO
            await _emailService.SendMail("", registerUserRequest.Email, "", "");
        }

        public async Task UpdateUserRole(int id, int roleId)
        {
            var existingRoles = await _rolesRepository.Get(roleId);
            if (existingRoles == null)
            {
                throw new InvalidDataException($"No existe un rol con el id {roleId}");

            }

            var updatedRows = await _usersRepository.UpdateUserRole(id, roleId);

            if (updatedRows == 0)
            {
                throw new InvalidDataException($"No existe un usuario con el id {id}");
            }
        }

    }
}
