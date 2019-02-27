using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Controllers;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using Xunit;

namespace RockPaperScissorsTests
{
    public class GamesControllerTest
    {
        private IGameService _gameService;
        private GamesController _controller;

        public GamesControllerTest()
        {
            _gameService = new GameService();
            _controller = new GamesController(_gameService);
        }

        [Fact]
        public void Post_WhenCalled_ReturnsGuid()
        {
            var player = new PlayerModel();
            player.Name = "Thomas";
            var result = _controller.Post(player);

            // Check so the result is a guid
            Assert.IsType<ActionResult<Guid>>(result);
        }

        [Fact]
        public void Post_WhenCalled_StatusIsNoGameFound()
        {
            var result = _controller.Status(Guid.NewGuid());

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.GameNotFound, result.Value.GameStatus);

        }

        [Fact]
        public void Post_WhenCalled_StatusIsWaitingForPlayerTwo()
        {
            var player = new PlayerModel();
            player.Name = "Thomas";
            var guid = _controller.Post(player);

            var result = _controller.Status(guid.Value);

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.WaitingForPlayerTwo, result.Value.GameStatus);
            
        }

        [Fact]
        public void Post_WhenCalledTwice_ReturnsGuidAndTwoActiveGames()
        {
            var player = new PlayerModel();
            player.Name = "Thomas";

            var result = _controller.Post(player);

            Assert.IsType<ActionResult<Guid>>(result);

            var player2 = new PlayerModel();
            player2.Name = "Sabine";

            var result2 = _controller.Post(player);

            Assert.IsType<ActionResult<Guid>>(result2);

            // Check so we don't get the same guid as well
            Assert.NotEqual(result, result2);

            // Check so we do have two running games, 
            Assert.Equal<int>(2, _gameService.GetGames().Count());
        }

        [Fact]
        public void Post_WhenJoined_ReturnsWaitingForAnyPlayer()
        {
            var player = new PlayerModel();
            player.Name = "Thomas";

            var guid = _controller.Post(player);

            var player2 = new PlayerModel();
            player2.Name = "Sabine";

            var result = _controller.Join(guid.Value, player2);

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.WaitingForAnyPlayerToPlay, result.Value.GameStatus);
        }

        [Fact]
        public void Post_WhenPlayerOnePlayed_ReturnsWaitingForSecondPlayer()
        {

            var player = new PlayerModel();
            player.Name = "Thomas";

            var guid = _controller.Post(player);

            var player2 = new PlayerModel();
            player2.Name = "Sabine";

            _controller.Join(guid.Value, player2);

            var move = new PlayerModel()
            {
                Name = "Thomas",
                Move = PlayerMove.Rock
            };

            var result = _controller.Move(guid.Value, move);

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.WaitingForSecondPlayerToPlay, result.Value.GameStatus);
        }

        [Fact]
        public void Post_WhenBothPlayerPlayed_ReturnsWinForPlayerOne()
        {

           

            var player = new PlayerModel();
            player.Name = "Thomas";

            var guid = _controller.Post(player);

            var player2 = new PlayerModel();
            player2.Name = "Sabine";

            _controller.Join(guid.Value, player2);

            var move = new PlayerModel()
            {
                Name = "Thomas",
                Move = PlayerMove.Rock
            };

            _controller.Move(guid.Value, move);

            var secondMove = new PlayerModel()
            {
                Name = "Sabine",
                Move = PlayerMove.Scissors
            };

            _controller.Move(guid.Value, secondMove);


            var result = _controller.Status(guid.Value, player);

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.GameFinished, result.Value.GameStatus);
            Assert.Equal(PlayerStatus.Win, result.Value.PlayerStatus);

            var result2 = _controller.Status(guid.Value, player);

            Assert.IsType<ActionResult<StatusModel>>(result2);
            Assert.Equal(GameStatus.GameFinished, result2.Value.GameStatus);
            Assert.Equal(PlayerStatus.Loss, result2.Value.PlayerStatus);
        }
    }
}
