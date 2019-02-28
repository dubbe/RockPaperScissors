using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Models
{
    /// <summary>
    /// Game model.
    /// </summary>
    public class GameModel
    {
        public Guid Id { get; set; }
        public IList<PlayerModel> Players { get; set; }
        public string ErrorMessage { get; set; }



        /// <summary>
        ///  Constructor for the gamemodel
        /// </summary>
        /// <param name="player">Starting player.</param>
        public GameModel(PlayerModel player)
        {
            // Cannot create game without a starting player and a name on starting player
            if(string.IsNullOrEmpty(player.Name))
            {
                throw new ArgumentException("Player name is required");
            }

            // Generate a unique id
            Id = Guid.NewGuid();

            // Initialize players and add first player
            Players = new List<PlayerModel>();
            Players.Add(player);

        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <returns><c>true</c>, if game was joined, <c>false</c> otherwise.</returns>
        /// <param name="player">Player.</param>
        public Boolean JoinGame(PlayerModel player)
        {
            if(Players.Any(p => p.Name == player.Name))
            {
                // Cannot add two players with the same name
                ErrorMessage = "Cannot add two players with the same name";
                return false;
            }

            if (Players.Count >= 2)
            {
                // Max number of players reached
                ErrorMessage = "Max number of players reached";
                return false;
            }

            Players.Add(player);
            return true;
        }

        /// <summary>
        /// Makes the move.
        /// </summary>
        /// <returns><c>true</c>, if move was made, <c>false</c> otherwise.</returns>
        /// <param name="move">Move.</param>
        public Boolean MakeMove(PlayerModel move)
        {
            PlayerModel player = GetPlayer(move.Name);

            if(player == null)
            {
                return false;
            }

            if (move.Move == null)
            {
                // illegal move
                return false;
            }

            // Make the move
            player.MakeMove(move.Move.Value);
             

            return true;
        }


        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns>The game status.</returns>
        public StatusModel GetStatus()
        {
            GameStatus status = _getGameStatus();
            switch(status)
            {
                case GameStatus.GameFinished:
                    return new StatusModel(Id, status);
                default:
                    return new StatusModel(Id, status);
            }

        }

      
        /// <summary>
        /// Gets the player. 
        /// </summary>
        /// <returns>The player.</returns>
        /// <param name="playerName">Player name.</param>
        public PlayerModel GetPlayer(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                // player name is empty
                return null;
            }
            return Players.FirstOrDefault(p => p.Name == playerName);
        }

        /// <summary>
        /// Gets the game result.
        /// </summary>
        /// <returns>The game result.</returns>
        /// <param name="player">The player that requested the result.</param>
        private PlayerStatus _getGameResult(PlayerModel player)
        {
            PlayerModel otherPlayer = Players.FirstOrDefault(p => p.Name != player.Name);
            return _getGameResult(player.Move.Value, otherPlayer.Move.Value);
        }

        /// <summary>
        /// Gets the game result.
        /// </summary>
        /// <returns>The game result.</returns>
        /// <param name="moveOne">Move one.</param>
        /// <param name="moveTwo">Move two.</param>
        private PlayerStatus _getGameResult(PlayerMove moveOne, PlayerMove moveTwo)
        {

            if (moveOne == moveTwo)
            {
                return PlayerStatus.Draw;
            } 

            if (((int)moveOne + 1) % 3 == (int)moveTwo)
            {
                return PlayerStatus.Loss;
            }
            else
            {
                return PlayerStatus.Win;
            }
        }

        /// <summary>
        /// Gets the game status.
        /// </summary>
        /// <returns>The game status.</returns>
        public GameStatus _getGameStatus()
        {
            if (Players.Count() == 1)
            {
                return GameStatus.WaitingForPlayerTwo;
            }

            switch (Players.Count(p => p.Move != null))
            {
                case 0:
                    return GameStatus.WaitingForAnyPlayerToPlay;
                case 1:
                    return GameStatus.WaitingForSecondPlayerToPlay;
                case 2:
                    // Game is finished
                    return GameStatus.GameFinished;
                default:
                    return GameStatus.StatusError;
            }
        }



    }
}
