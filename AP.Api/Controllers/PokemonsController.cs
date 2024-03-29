using System.Threading.Tasks;
using AP.Core.Domain;
using AP.Core.Shared.ModelViews;
using AP.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonManager pokemonManager;

        public PokemonsController(IPokemonManager pokemonManager)
        {
            this.pokemonManager = pokemonManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await pokemonManager.GetPokemonsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await pokemonManager.GetPokemonAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(NovoPokemon pokemon)
        {
            var pokemonInserido = await pokemonManager.InsertPokemonAsync(pokemon);
            return CreatedAtAction(nameof(Get), new { id = pokemonInserido.Id }, pokemonInserido); 
        }

        [HttpPut]
        public async Task<IActionResult> Put(AlteraPokemon alteraPokemon)
        {
            var pokemonAtualizado = await pokemonManager.UpdatePokemonAsync(alteraPokemon);
            if(pokemonAtualizado == null)
            {
                return NotFound();
            }
            return Ok(pokemonAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await pokemonManager.DeletePokemonAsync(id);
            return NoContent();
        }

    }
}