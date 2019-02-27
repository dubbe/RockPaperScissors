using System;
namespace RockPaperScissors.Models
{
    public enum GameStatus
    {
        GameNotFound,
        WaitingForPlayerTwo,
        WaitingForAnyPlayerToPlay,
        WaitingForSecondPlayerToPlay,
        GameFinished
    }

    public enum PlayerStatus
    {
        WaitingToPlay,
        WaitingForOtherPlayer,
        WonGame,
        LostGame,
        Draw
    }

    public class StatusModel
    {
        public GameStatus GameStatus { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
       
        public StatusModel()
        {

        }
        public StatusModel(GameStatus status)
        {
            GameStatus = status;
        }

    }
}
