using FluentValidation;
using PlantillaBack.Entities.Artists;
using PlantillaBack.Entities.Artists.Requests.Create;
using PlantillaBack.Entities.Artists.Requests.Update;
using PlantillaBack.Repositories.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Servicies.Artists
{
    public class ArtistsServices : IArtistsServices
    {
        readonly IArtistsRepository _artistsRepository;

        public ArtistsServices(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public async Task<int> Create(CreateArtistRequest createArtistRequest)
        {
            var validator = new CreateArtistRequestValidator();
            var result = await validator.ValidateAsync(createArtistRequest);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var existingArtist = await _artistsRepository.Get(createArtistRequest.Name);
            if (existingArtist != null)
            {
                throw new InvalidDataException($"Ya existe un artista con el nombre {createArtistRequest.Name}");
            }

            var artist = new Artist() { Name = createArtistRequest.Name };
            return await _artistsRepository.Create(artist);


        }

        public async Task Delete(int id)
        {
            var updatedRows = await _artistsRepository.Delete(id);

            if (updatedRows == 0)
            {
                throw new InvalidDataException($"No existe un artista con el id {id}");
            }
        }

        public async Task<Artist?> Get(int id)
        {
            return await _artistsRepository.Get(id);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _artistsRepository.GetAll();

        }

        public async Task Update(int id, UpdateArtistRequest updateArtistRequest)
        {
            var validator = new UpdateArtistRequestValidator();
            var result = await validator.ValidateAsync(updateArtistRequest);

            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var updateRows = await _artistsRepository.Update(id, updateArtistRequest);

            if(updateRows == 0)
            {
                throw new InvalidDataException($"No existe un artista con el id {id}");
            }
        }
    }
}
