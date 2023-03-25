using Microsoft.EntityFrameworkCore;
using PlantillaBack.Entities.Artists;
using PlantillaBack.Entities.Artists.Requests.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlantillaBack.Repositories.Artists
{
    public class ArtistsRepository : IArtistsRepository
    {
        //? Con "readonly DataBaseContext" lo que hacemos es declarar nuestra conexión a la BBDD que hemos configurado
        //? con anterioridad

        readonly DataBaseContext _context;

        public ArtistsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();

            return artist.Id;
        }

        public async Task<int> Delete(int id)
        {
            return await _context.Artists
                   .Where(x => x.Id == id)
                   .ExecuteDeleteAsync();
        }

        public async Task<Artist?> Get(string name)
        {
            return await _context.Artists.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

        }

        public async Task<Artist?> Get(int id)
        {
            return await _context.Artists.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _context.Artists.ToListAsync();

        }

        public async Task<int> Update(int id, UpdateArtistRequest updateArtistRequest)
        {
            return await _context.Artists
                  .Where(x => x.Id == id)
                  .ExecuteUpdateAsync(artist => artist.SetProperty
                  (property => property.Name,
                  property => updateArtistRequest.Name));
        }
    }
}
