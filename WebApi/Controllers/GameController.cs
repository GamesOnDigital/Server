using BLL.Interfaces;
using DTO.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {

        private readonly IGameBll _game;

        public GameController(IGameBll game)
        {
            _game = game;
        }

        //[Authorize]
        [HttpPost]
        public async Task<Game> Add(Game game)
        {

            if (game == null)
               throw new ArgumentNullException(nameof(game));
            Game game1 = await _game.Add(game);
            return game1;
        }

        [HttpPut("{id}")]
        public async Task<Game> Update(Game updatedGame)
        {
            return await _game.Update(updatedGame);

        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _game.Delete(id);
        }

        [HttpGet("{id}")]
        public async Task<Game> GetById(int id)
        {
            return await _game.GetById(id);
        }

        [HttpGet()]
        public async Task<List<Game>> GetAll()
        {
            return await _game.GetAll();
        }
    }

}

