using System;
using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{
    public enum PlayerMove
    {
        Rock = 0, Paper = 1, Scissors = 2
    }
    public class PlayerModel
    {

        [Required]
        public string Name { get; set; }

        public PlayerMove? Move { get; set; }
        public PlayerStatus Status { get; set; }

        public PlayerModel()
        {
        }

        public Boolean MakeMove(PlayerMove move)
        {
            Status = PlayerStatus.WaitingForOtherPlayer;
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
