using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{
    /// <summary>
    /// Allowed moves
    /// </summary>
    public enum PlayerMove
    {
        Rock = 0, 
        Paper = 1, 
        Scissors = 2
    }

    /// <summary>
    /// The states a player can be in
    /// </summary>
    public enum PlayerStatus {
        [Description("Has not made a move")]
        HasNotMadeMove,
        [Description("Has made a move")]
        HasMadeMove,
        [Description("Played {0} and won the game")]
        Win,
        [Description("Played {0} and lost the game")]
        Loss,
        [Description("Played {0}, game ended in a draw")]
        Draw
    }

    /// <summary>
    /// Class to hold everything about a player
    /// </summary>
    public class PlayerModel
    {

        [Required]
        public string Name { get; set; }

        private PlayerMove? _move { get; set; }

        public PlayerMove Move { set {
            _move = value;
        }}

        private PlayerStatus _status { get; set; }
        public string Status { get {
            // Return the descriptor
            return string.Format(_status.ToDescriptionString(), _move.ToString());
        }}

        public PlayerModel()
        {
            // When initialized a player has not yet made a move
            _status = PlayerStatus.HasNotMadeMove;
        }

        /// <summary>
        /// Make the move
        /// </summary>
        /// <param name="move">The move to make</param>
        /// <returns><c>true</c>, if move was made, <c>false</c> otherwise.</returns>
        public Boolean MakeMove(PlayerMove move)
        {
            if (_move != null)
            {
                // player already made move
                return false;
            }
            _move = move;

            // Set status that player has made the move
            _status = PlayerStatus.HasMadeMove;
            return true;
        }

        /// <summary>
        /// Setter for staus
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(PlayerStatus status) {
            _status = status;
        }

        /// <summary>
        /// Getter for status, mostly used for testing
        /// </summary>
        /// <returns></returns>
        public PlayerStatus GetStatus() {
            return _status;
        }

        /// <summary>
        /// Getter for move, have to obscure moves so that it's not possible to view the move before the game is finished
        /// </summary>
        /// <returns>Move the player made</returns>
        public PlayerMove? GetMove() {
            return _move;
        }

    }
}
