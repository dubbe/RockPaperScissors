using System;
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
        HasNotMadeMove,
        HasMadeMove,
        Win,
        Loss,
        Draw
    }

    public class PlayerModel
    {

        [Required]
        public string Name { get; set; }

        public PlayerMove? Move { get; set; }
        public PlayerStatus Status { get; set; }

        public PlayerModel()
        {
            Status = PlayerStatus.HasNotMadeMove;
        }

        public Boolean MakeMove(PlayerMove move)
        {
            Status = PlayerStatus.HasMadeMove;
            if (Move != null)
            {
                // player already made move
                return false;
            }
            Move = move;
            return true;
        }

    }
}
