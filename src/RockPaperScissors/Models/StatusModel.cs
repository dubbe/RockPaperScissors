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

    public enum PlayerStatus
    {
        WaitingToPlay,
        WaitingForOtherPlayer,
        Win,
        Loss,
        Draw
    }

    public class StatusModel
    {
        public GameStatus GameStatus { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
       
        public StatusModel()
        {

        }
        public StatusModel(GameStatus gameStatus)
        {
            GameStatus = gameStatus;
        }
        public StatusModel(GameStatus gameStatus, PlayerStatus playerStatus)
        {
            GameStatus = gameStatus;
            PlayerStatus = playerStatus;
        }


    }
}
