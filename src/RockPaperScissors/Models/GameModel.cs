﻿using System;
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
        public GameModel(string name)
        {
            // Cannot create game without a starting player and a name on starting player
            if(string.IsNullOrEmpty(name))
            {
                // Don't want to create an instance at all, so throw an exception
                throw new ArgumentException("Player name is required");
            }

            // Generate a unique id
            Id = Guid.NewGuid();

            // Initialize players-list and add first player
            Players = new List<PlayerModel>();
            Players.Add(new PlayerModel() {
                Name = name
            });

        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <returns><c>true</c>, if game was joined, <c>false</c> otherwise.</returns>
        /// <param name="player">Player.</param>
        public Boolean JoinGame(string name)
        {
            if(Players.Any(p => p.Name == name))
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

            Players.Add(new PlayerModel() {
                Name = name
            });
            return true;
        }

        /// <summary>
        /// Makes the move. Nothing fancy, just updates the player object with the move
        /// </summary>
        /// <returns><c>true</c>, if move was made, <c>false</c> otherwise.</returns>
        /// <param name="move">Move.</param>
        public Boolean MakeMove(string name, PlayerMove move)
        {
            PlayerModel player = GetPlayer(name);

            if(player == null)
            {
                ErrorMessage = "Could not find the player in this game";
                return false;
            }

            // Make the move
            player.MakeMove(move);
             

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
                    // Find out who won the game
                    _getGameResult();
                    return new StatusModel(Id, status, Players);
                default:
                    return new StatusModel(Id, status, Players);
            }

        }

      
        /// <summary>
        /// Gets the player based on the name
        /// </summary>
        /// <returns>The player.</returns>
        /// <param name="playerName">Player name.</param>
        public PlayerModel GetPlayer(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                // player name is empty
                ErrorMessage = "Cannot create player without a name";
                return null;
            }
            return Players.FirstOrDefault(p => p.Name == playerName);
        }

        /// <summary>
        /// Runs the game and sets the result on the players
        /// This is the whole-game mecanics really. A check using a modulus operator on player one and giving each move a number
        /// 0-2 makes it possible to check if player twos move won over player one. 
        /// </summary>
        private void _getGameResult()
        {
            if(Players.Count() != 2) {
                // Has to have two players to work.
                ErrorMessage = "Cannot get result from game without two players";
                return;
            }

            PlayerModel playerOne = Players[0];
            PlayerModel playerTwo = Players[1];

            if (playerOne.GetMove() == playerTwo.GetMove())
            {
                playerOne.SetStatus(PlayerStatus.Draw);
                playerTwo.SetStatus(PlayerStatus.Draw);
            } else  if (((int)playerOne.GetMove() + 1) % 3 == (int)playerTwo.GetMove())
            {
                playerOne.SetStatus(PlayerStatus.Loss);
                playerTwo.SetStatus(PlayerStatus.Win);
            }
            else
            {
                playerOne.SetStatus(PlayerStatus.Win);
                playerTwo.SetStatus(PlayerStatus.Loss);
            }
            return;

        }

        /// <summary>
        /// Gets the game status. That is what state the game are in right now.
        /// </summary>
        /// <returns>The game status.</returns>
        public GameStatus _getGameStatus()
        {
            if (Players.Count() == 1)
            {
                return GameStatus.WaitingForPlayerTwo;
            }

            switch (Players.Count(p => p.GetMove() != null))
            {
                case 0:
                    return GameStatus.WaitingForAnyPlayerToPlay;
                case 1:
                    return GameStatus.WaitingForSecondPlayerToPlay;
                case 2:
                    // Game is finished
                    return GameStatus.GameFinished;
                default:
                    ErrorMessage = "Unkown error";
                    return GameStatus.Error;
            }
        }



    }
}
