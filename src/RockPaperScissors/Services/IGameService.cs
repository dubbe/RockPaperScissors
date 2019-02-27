using System;
using System.Collections.Generic;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        IList<GameModel> GetGames();
        GameModel GetGame(Guid guid);
        GameModel StartGame(PlayerModel player);
        StatusModel JoinGame(Guid guid, PlayerModel player);
       
    }
}
