using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Models;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    public class Player {
        [Required]
        public string Name {get;set;}
    }

    public class GameMove {
        [Required]
        public string Name {get;set;}
        public PlayerMove Move {get;set;}
    }

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
        public ActionResult<Guid> Post([FromBody] Player player)
        {
            // Create the game
            try {
                var game = _gameService.StartGame(player.Name);
                return game.Id;
            } catch(ArgumentException) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when creating game");
            }
        }

        // GET /api/games/{id}
        // Get status of game
        [HttpGet("{id}")]
        public ActionResult<StatusModel> Status(string id)
        {
            GameModel game = _gameService.GetGame(id);
            if(game == null)
            {
                // no game found
                return new StatusModel(id, GameStatus.GameNotFound);
            }
            return game.GetStatus();
        }
        
        // Post /api/games/{id}/join
        // Join an existing game
        [HttpPost("{id}/join")]
        public IActionResult Join(string id, [FromBody] Player player)
        {
            GameModel game = _gameService.GetGame(id);
            
            if (game == null)
                return NotFound("Game not found");

            if(!game.JoinGame(player.Name))
                return BadRequest(game.ErrorMessage);

            return Ok(game.GetStatus());
        }

        // Post /api/games/{id}/move
        // Make your move
        [HttpPost("{id}/move")]
        public ActionResult<StatusModel> Move(string id, [FromBody] GameMove move)
        {
            GameModel game = _gameService.GetGame(id);
            if (game == null)
            {
                // no game found
                return new StatusModel(new Guid(id), GameStatus.GameNotFound);
            }

            game.MakeMove(move.Name, move.Move);
            return game.GetStatus();
        }


    }
}

