using System;
namespace RockPaperScissors.Models
{
    public enum Status
    {
        GameNotFound,
        WaitingForPlayerTwo,
        WaitingForAnyPlayerToPlay,
        WaitingForSecondPlayerToPlay,
        GameFinished
    }

    public class StatusModel
    {
        public Status Status { get; set; }
        public StatusModel()
        {

        }
        public StatusModel(Status status)
        {
            Status = status;
        }

    }
}
