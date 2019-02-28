using System;
using System.Collections.Generic;

namespace RockPaperScissors.Models
{
    public enum GameStatus
    {
        GameNotFound,
        PlayerNotFoundInGame,
        WaitingForPlayerTwo,
        WaitingForAnyPlayerToPlay,
        WaitingForSecondPlayerToPlay,
        GameFinished,
        StatusError
    }

    public class StatusModel
    {
        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public IList<PlayerModel> Players {get;set;}
        
        public StatusModel()
        {

        }
        public StatusModel(Guid id, GameStatus gameStatus)
        {
            Id = id;
            GameStatus = gameStatus;
        }

    }
}
