using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Entities.Artists.Requests.Update
{
    public class UpdateArtistRequestValidator : AbstractValidator<UpdateArtistRequest>
    {
        public UpdateArtistRequestValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

        }
    }
}
