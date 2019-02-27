using System;
namespace RockPaperScissors.Models
{
    public enum PlayerMove
    {
        Rock, Paper, Scissors
    }
    public class PlayerModel
    {

        public string Name { get; set; }
        public PlayerMove? Move { get; set; }

        public PlayerModel()
        {
        }

        public Boolean MakeMove(PlayerMove move)
        {
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
