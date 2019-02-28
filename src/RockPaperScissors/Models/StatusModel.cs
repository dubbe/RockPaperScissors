using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RockPaperScissors.Models
{
    /// <summary>
    /// States a game can be in
    /// </summary>
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

        /// <summary>
        /// Create a status from a Guid-id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameStatus"></param>
        /// <param name="players">Optional, default value is null</param>
        public StatusModel(Guid id, GameStatus gameStatus, IList<PlayerModel> players = null)
        {
            Id = id;
            _gameStatus = gameStatus;
            Players = players;
        }

        /// <summary>
        /// Create a status from a string id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameStatus"></param>
        /// <param name="players">Optional, default value is null</param>
        public StatusModel(string id, GameStatus gameStatus, IList<PlayerModel> players = null)
        {
            Id = new Guid(id);
            _gameStatus = gameStatus;
            Players = players;
        }

    }
}
