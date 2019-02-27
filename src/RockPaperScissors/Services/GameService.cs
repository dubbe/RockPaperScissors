﻿using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private IList<GameModel> _games;
        public GameService()
        {
            _games = new List<GameModel>();
        }

        public IList<GameModel> GetGames()
        {
            return _games;
        }

        public GameModel GetGame(Guid guid)
        {
            return _games.FirstOrDefault(g => g.Id == guid);
        }

        public GameModel StartGame(PlayerModel player)
        {
            var game = new GameModel(player);
            _games.Add(game);
            return game;
        }

        //public StatusModel GetStatus(Guid guid)
        //{
        //    var game = _getGame(guid);
        //    if (game == null)
        //    {
        //        return new StatusModel(Status.GameNotFound);
        //    }
        //    return game.GetStatus();
        //}

        public GameModel JoinGame(Guid guid, PlayerModel player)
        {
            var game = GetGame(guid);
            if(game != null)
            {
                game.JoinGame(player);
            }
            return game;
        }

        //public StatusModel MakeMove(Guid guid, MoveModel move)
        //{
        //    var game = _getGame(guid);
        //    if(game == null)
        //    {
        //        return new StatusModel(Status.GameNotFound);
        //    }
        //    // Some error handling to se of player already made a move!
        //    game.MakeMove(move);
        //    return new StatusModel(Status.GameNotFound);

        //}

        //public IList<GameModel> GetGames()
        //{
        //    return _games;
        //}

    }
}
