using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RockPaperScissors.Models
{
    public enum GameStatus
    {
        [Description("Game not found")]
        GameNotFound,
        [Description("Waiting for player two")]
        WaitingForPlayerTwo,
        [Description("Waiting for any player to play")]
        WaitingForAnyPlayerToPlay,
        [Description("Waiting for second player to play")]
        WaitingForSecondPlayerToPlay,
        [Description("Game finished")]
        GameFinished,
        Error
    }

    public class StatusModel
    {
        public Guid Id { get; set; }
        private GameStatus _gameStatus { get; set;}
        public string Status { get {
            return _gameStatus.ToDescriptionString();
        }}
        public IList<PlayerModel> Players {get;set;}
        
        public StatusModel()
        {

        }
        public StatusModel(Guid id, GameStatus gameStatus, IList<PlayerModel> players = null)
        {
            Id = id;
            _gameStatus = gameStatus;
            Players = players;
        }

        public StatusModel(string id, GameStatus gameStatus, IList<PlayerModel> players = null)
        {
            Id = new Guid(id);
            _gameStatus = gameStatus;
            Players = players;
        }

    }
}
