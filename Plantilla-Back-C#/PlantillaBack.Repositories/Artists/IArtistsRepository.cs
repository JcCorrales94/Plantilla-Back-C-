using PlantillaBack.Entities.Artists;
using PlantillaBack.Entities.Artists.Requests.Update;

namespace PlantillaBack.Repositories.Artists
{
    public interface IArtistsRepository
    {
        Task<IEnumerable<Artist>> GetAll();

        Task<int> Create(Artist artist);
        Task<Artist?> Get(string name);
        Task<Artist?> Get(int id);
        Task<int> Update(int id, UpdateArtistRequest updateArtistRequest);
        Task<int> Delete(int id);
    }
}
