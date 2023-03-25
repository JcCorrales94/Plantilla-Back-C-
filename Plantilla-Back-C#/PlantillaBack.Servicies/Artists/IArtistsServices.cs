using PlantillaBack.Entities.Artists;
using PlantillaBack.Entities.Artists.Requests.Create;
using PlantillaBack.Entities.Artists.Requests.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Servicies.Artists
{
    public interface IArtistsServices
    {
        Task<IEnumerable<Artist>> GetAll();

        Task<int> Create(CreateArtistRequest createArtistRequest);

        Task<Artist?> Get(int id);

        Task Update(int id, UpdateArtistRequest updateArtistRequest);

        Task Delete(int id);
    }
}
