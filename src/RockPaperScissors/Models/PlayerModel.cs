using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{
    public enum PlayerMove
    {
        Rock = 0, 
        Paper = 1, 
        Scissors = 2
    }

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
            return string.Format(_status.ToDescriptionString(), _move.ToString());
        }}

        public PlayerModel()
        {
            _status = PlayerStatus.HasNotMadeMove;
        }

        public Boolean MakeMove(PlayerMove move)
        {
            _status = PlayerStatus.HasMadeMove;
            if (_move != null)
            {
                // player already made move
                return false;
            }
            _move = move;
            return true;
        }

        public void SetStatus(PlayerStatus status) {
            _status = status;
        }

        public PlayerStatus GetStatus() {
            return _status;
        }

        public PlayerMove? GetMove() {
            return _move;
        }

    }
}
