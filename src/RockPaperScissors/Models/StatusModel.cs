using System;
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
        public GameStatus GameStatus { get; set; }
       
        public StatusModel()
        {

        }
        public StatusModel(GameStatus gameStatus)
        {
            GameStatus = gameStatus;
        }

    }
}
