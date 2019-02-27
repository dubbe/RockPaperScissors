using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public IList<PlayerModel> Players { get; set; }

        public GameModel(PlayerModel player)
        {
            // Generate a unique id
            Id = Guid.NewGuid();

            // Initialize players and add first player
            Players = new List<PlayerModel>();
            Players.Add(player);

        }

        public Boolean JoinGame(PlayerModel player)
        {
            if(Players.Count >= 2)
            {
                // Max number of players reached
                return false;
            }

            Players.Add(player);
            return true;
        }

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

        public StatusModel GetStatus()
        {
            return new StatusModel(_getGameStatus());

        }


        public StatusModel GetStatus(PlayerModel player)
        {
            player = GetPlayer(player.Name);
            if (player == null)
            {
                return new StatusModel(GameStatus.PlayerNotFoundInGame);
            }

            switch (_getGameStatus())
            {
                case GameStatus.WaitingForPlayerTwo:
                    return new StatusModel(GameStatus.WaitingForPlayerTwo, PlayerStatus.WaitingForOtherPlayer);
                case GameStatus.WaitingForAnyPlayerToPlay:
                    return new StatusModel(GameStatus.WaitingForAnyPlayerToPlay, PlayerStatus.WaitingToPlay);
                case GameStatus.WaitingForSecondPlayerToPlay:
                    return new StatusModel(GameStatus.WaitingForSecondPlayerToPlay, player.Move != null ? PlayerStatus.WaitingToPlay : PlayerStatus.WaitingForOtherPlayer);
                case GameStatus.GameFinished:
                    return new StatusModel(GameStatus.GameFinished, _getGameResult(player));
                default:
                    return new StatusModel(GameStatus.StatusError);
            }
           
        }

        private PlayerStatus _getGameResult(PlayerModel player)
        {
            PlayerModel otherPlayer = Players.FirstOrDefault(p => p.Name != player.Name);
            return _getGameResult(player.Move.Value, otherPlayer.Move.Value);
        }

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

        public PlayerModel GetPlayer(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                // playername is empty
                return null;
            }
            return Players.FirstOrDefault(p => p.Name == playerName);
        }

    }
}
