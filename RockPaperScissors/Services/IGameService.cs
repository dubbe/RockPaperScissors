using System;
using System.Collections.Generic;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        GameModel StartGame(PlayerModel player);
        IList<GameModel> GetGames();
        StatusModel GetStatus(Guid id);
        StatusModel JoinGame(Guid id, PlayerModel player);
        
    }
}
