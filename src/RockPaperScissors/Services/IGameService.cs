using System;
using System.Collections.Generic;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        IList<GameModel> GetGames();
        GameModel GetGame(string guid);
        GameModel GetGame(Guid guid);
        GameModel StartGame(string name);
        //StatusModel JoinGame(Guid guid, PlayerModel player);
       
    }
}
