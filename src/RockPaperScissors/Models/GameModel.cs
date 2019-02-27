using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public IList<PlayerModel> Players { get; set; }

        private StatusModel _status;

        public GameModel(PlayerModel player)
        {
            // Generate a unique id
            Id = Guid.NewGuid();

            // Initialize players and add first player
            Players = new List<PlayerModel>();
            Players.Add(player);

            // Set status to waiting for player two
            _status = new StatusModel(GameStatus.WaitingForPlayerTwo);
        }

        public Boolean JoinGame(PlayerModel player)
        {
            if(Players.Count >= 2)
            {
                // Max number of players reached
                return false;
            }

            Players.Add(player);
            return true;
            //_status = new StatusModel(Status.WaitingForPlayerTwo);
        }

        public Boolean MakeMove(PlayerModel move)
        {
            PlayerModel player = _getPlayer(move);
            if(player == null)
            {
                // this player is not in this game
                return false;
            }

            if(move.Move == null)
            {
                // illegal move
                return false;
            }

            return player.MakeMove(move.Move.Value);
        }

        public StatusModel GetStatus()
        {
            return _status;
        }

        private PlayerModel _getPlayer(PlayerModel player)
        {
            return Players.FirstOrDefault(p => p.Name == player.Name);
        }

    }
}
