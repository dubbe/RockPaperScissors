using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private IList<GameModel> _games;
        public GameService()
        {
            _games = new List<GameModel>();
        }

        private GameModel _getGame(Guid guid)
        {
            return _games.FirstOrDefault(g => g.Id == guid);
        }

        public GameModel StartGame(PlayerModel player)
        {
            var game = new GameModel(player);
            _games.Add(game);
            return game;
        }

        public StatusModel GetStatus(Guid guid)
        {
            var game = _getGame(guid);
            if (game == null)
            {
                return new StatusModel(Status.GameNotFound);
            }
            return game.GetStatus();
        }

        public StatusModel JoinGame(Guid guid, PlayerModel player)
        {
            var game = _getGame(guid);
            if (game == null)
            {
                return new StatusModel(Status.GameNotFound);
            }

            game.JoinGame(player);
            return game.GetStatus();
        }

        public IList<GameModel> GetGames()
        {
            return _games;
        }

    }
}
