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

        public IList<GameModel> GetGames()
        {
            return _games;
        }

        public GameModel GetGame(Guid guid)
        {
            return _games.FirstOrDefault(g => g.Id == guid);
        }

        public GameModel GetGame(string id)
        {
            Guid guid = new Guid(id);
            return GetGame(guid);
        }

        public GameModel StartGame(string name)
        {
            var game = new GameModel(name);
            _games.Add(game);
            return game;
        }

    }
}
