using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Models;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // POST api/games
        // Start a new game
        [HttpPost]
        public ActionResult<Guid> Post([FromBody] PlayerModel player)
        {
            // Create the game
            var game = _gameService.StartGame(player);
            return game.Id;
        }

        // GET /api/games/{id}
        // Get status of game
        [HttpGet("{id}")]
        public ActionResult<StatusModel> Status(Guid guid)
        {
            return _gameService.GetStatus(guid);
        }

        // Post /api/games/{id}/join
        // Join an existing game
        [HttpPost("{id}/join")]
        public ActionResult<StatusModel> Join(Guid guid, [FromBody] PlayerModel player)
        {
            return  _gameService.JoinGame(guid, player);
        }

        // Post /api/games/{id}/move
        // Make your move
        [HttpPost("{id}/move")]
        public ActionResult<StatusModel> Move(Guid guid, [FromBody] MoveModel move)
        {
            return _gameService.MakeMove(guid, move);
        }


    }
}
