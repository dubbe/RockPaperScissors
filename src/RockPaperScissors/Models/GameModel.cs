using System;
namespace RockPaperScissors.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public PlayerModel PlayerOne { get; set; }
        public PlayerModel PlayerTwo { get; set; }

        private StatusModel _status;

        public GameModel(PlayerModel player)
        {
            // Generate a unique id
            Id = Guid.NewGuid();

            // Add the first player
            PlayerOne = player;

            // Set status to waiting for player two
            _status = new StatusModel(Status.WaitingForPlayerTwo);
        }

        public void JoinGame(PlayerModel player)
        {
            PlayerTwo = player;
            _status = new StatusModel(Status.WaitingForPlayerTwo);
        }

        public StatusModel GetStatus()
        {
            return _status;
        }

    }
}
