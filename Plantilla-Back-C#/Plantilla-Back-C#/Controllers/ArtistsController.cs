using Microsoft.AspNetCore.Mvc;
using PlantillaBack.Entities.Artists.Requests.Create;
using PlantillaBack.Entities.Artists.Requests.Update;
using PlantillaBack.Entities.Artists;
using PlantillaBack.Servicies.Artists;
using Microsoft.AspNetCore.Authorization;

namespace Plantilla_Back_C_.Controllers
{
    //? DE ESTA MANERA PEDIMOS QUE EL USUARIO NOS PASE UNA AUTORIZACIÓN PARA REALIZAR EL POST (PEDIMOS EL TOKEN)
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        readonly IArtistsServices _artistsService;

        public ArtistsController(IArtistsServices artistsService)
        {
            _artistsService = artistsService;
        }

        [HttpGet]
        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _artistsService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<Artist?> Get(int id)
        {
            return await _artistsService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateArtistRequest artist)
        {
            var id = await _artistsService.Create(artist);

            return Created($"/api/artists/{id}", null);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateArtistRequest artist)
        {
            await _artistsService.Update(id, artist);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _artistsService.Delete(id);
            return Ok();
        }
    }
}
