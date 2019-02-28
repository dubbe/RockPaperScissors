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
        /// <summary>
        /// Not used, but could be a nice addition
        /// </summary>
        /// <returns>List of all games</returns>
        public IList<GameModel> GetGames()
        {
            return _games;
        }
        /// <summary>
        /// Get the wanted game
        /// </summary>
        /// <param name="guid">game-id, a Guid</param>
        /// <returns>Game object</returns>
        public GameModel GetGame(Guid guid)
        {
            return _games.FirstOrDefault(g => g.Id == guid);
        }
        /// <summary>
        /// Get the wanted game
        /// </summary>
        /// <param name="id">game-id, a string</param>
        /// <returns>Game object</returns>
        public GameModel GetGame(string id)
        {
            Guid guid = new Guid(id);
            return GetGame(guid);
        }

        /// <summary>
        ///  Start a new game
        /// </summary>
        /// <param name="name">The name of the first player</param>
        /// <returns>Game object</returns>
        public GameModel StartGame(string name)
        {
            var game = new GameModel(name);
            _games.Add(game);
            return game;
        }

    }
}
