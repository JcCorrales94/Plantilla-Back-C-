using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlantillaBack.Entities.Users.Requests.Create
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        //? La libreria AbstractValidator nos permite comprobar que los parámetros de una clase cumplan ciertos 
        //? requisitos. Además podemos crear nuestras propias condiciones a cumplir como podemos observar en el
        //? método "HasValidPassword"

        const int PasswordMinLength = 8;

        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Password)
                .Equal(x => x.RepeatPassword)
                .WithMessage("Ambas contraseña deben ser identicas.")
                .MinimumLength(PasswordMinLength)
                .Must(HasValidPassword)
                .WithMessage($"El password no cumple los criterios de longitud {PasswordMinLength} uso de mayúsculas, minúsculas y un dígito");

        }

        private static bool HasValidPassword(string pw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");

            return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw));
        }
    }
}
