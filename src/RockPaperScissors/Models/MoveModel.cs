using System;
namespace RockPaperScissors.Models
{
    public enum Move
    {
        Rock, Paper, Scissors
    }
    public class MoveModel : PlayerModel
    {
        public Move Move { get; set; }
        public MoveModel()
        {
        }
    }
}
