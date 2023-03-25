using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Entities.Artists.Requests.Create
{
    public class CreateArtistRequestValidator : AbstractValidator<CreateArtistRequest>
    {

        //? La libreria AbstractValidator nos permite comprobar que los parámetros de una clase cumplan ciertos 
        //? requisitos
        public CreateArtistRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
