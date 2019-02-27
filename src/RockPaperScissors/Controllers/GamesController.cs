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
            System.Diagnostics.Debugger.Break();
            System.Diagnostics.Debug.WriteLine("testar");

            // Create the game
            var game = _gameService.StartGame(player);
            return game.Id;
        }

        // GET /api/games/{id}
        // Get status of game
        [HttpGet("{id}")]
        public ActionResult<StatusModel> Status(Guid guid)
        {


            GameModel game = _gameService.GetGame(guid);
            if(game == null)
            {
                // no game found
                return new StatusModel(GameStatus.GameNotFound);
            }
            return game.GetStatus();
        }

        // GET /api/games/{id}
        // Get status of game
        [HttpGet("{id}")]
        public ActionResult<StatusModel> Status(Guid guid, [FromBody] PlayerModel player)
        {
            GameModel game = _gameService.GetGame(guid);
            if (game == null)
            {
                // no game found
                return new StatusModel(GameStatus.GameNotFound);
            }

            player = game.GetPlayer(player.Name);
            if (player == null)
            {
                return new StatusModel(GameStatus.PlayerNotFoundInGame);
            }
            return game.GetStatus(player);
        }

        // Post /api/games/{id}/join
        // Join an existing game
        [HttpPost("{id}/join")]
        public ActionResult<StatusModel> Join(Guid guid, [FromBody] PlayerModel player)
        {
            GameModel game = _gameService.GetGame(guid);
            if (game == null)
            {
                // no game found
                return new StatusModel(GameStatus.GameNotFound);
            }

            game.JoinGame(player);
            return game.GetStatus();
        }

        // Post /api/games/{id}/move
        // Make your move
        [HttpPost("{id}/move")]
        public ActionResult<StatusModel> Move(Guid guid, [FromBody] PlayerModel move)
        {
            GameModel game = _gameService.GetGame(guid);
            if (game == null)
            {
                // no game found
                return new StatusModel(GameStatus.GameNotFound);
            }

            game.MakeMove(move);
            return game.GetStatus();
        }


    }
}

