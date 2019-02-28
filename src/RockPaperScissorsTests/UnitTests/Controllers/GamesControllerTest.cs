using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Controllers;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using Xunit;

namespace RockPaperScissorsTests.UnitTests.Controllers
{
    public class GamesControllerTest
    {
        private IGameService _gameService;
        private GamesController _controller;

        private Player _playerOne;
        private Player _playerTwo;
        private Player _playerThree;

        public GamesControllerTest()
        {
            _gameService = new GameService();
            _controller = new GamesController(_gameService);

            _playerOne = new Player()
            {
                Name = "Thomas"
            };

            _playerTwo = new Player()
            {
                Name = "Sabine"
            };

            _playerThree = new Player()
            {
                Name = "Benjamin"
            };
        }

        [Fact]
        public void Post_WhenCalled_ReturnsGuid()
        {

            var result = _controller.Post(_playerOne);

            // Check so the result is a guid
            Assert.IsType<ActionResult<Guid>>(result);
        }

        [Fact]
        public void Post_WhenCalledWithNoName_ReturnsStatusCode500()
        {

             var result = _controller.Post(new Player());

            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);

            // Check so statuscode is 500
            Assert.Equal("500", objectResult.StatusCode.Value.ToString());
        }

        [Fact]
        public void Post_WhenCalledTwiceAndWithNoName_ReturnsTwoGames()
        {
            _controller.Post(_playerOne);
            var result = _controller.Post(new Player());
            _controller.Post(_playerTwo);

            // Check so we do have two running games, 
            Assert.Equal<int>(2, _gameService.GetGames().Count());
        }

        [Fact]
        public void Status_WhenCalled_StatusIsWaitingForPlayerTwo()
        {

            var guid = _controller.Post(_playerOne);

            var result = _controller.Status(guid.Value.ToString());

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.WaitingForPlayerTwo.ToDescriptionString(), result.Value.Status);
            
        }

        [Fact]
        public void Post_WhenCalledTwice_ReturnsGuidAndTwoActiveGames()
        {

            var result = _controller.Post(_playerOne);

            Assert.IsType<ActionResult<Guid>>(result);


            var result2 = _controller.Post(_playerTwo);

            Assert.IsType<ActionResult<Guid>>(result2);

            // Check so we don't get the same guid as well
            Assert.NotEqual(result, result2);

            // Check so we do have two running games, 
            Assert.Equal<int>(2, _gameService.GetGames().Count());
        }

        [Fact]
        public void Join_WhenCalled_ReturnsWaitingForAnyPlayer()
        {
            var guid = _controller.Post(_playerOne);

            var result = _controller.Join(guid.Value.ToString(), _playerTwo);

            var okObject = result as OkObjectResult;

            Assert.NotNull(okObject);
            
            var status = okObject.Value as StatusModel;

            Assert.Equal(GameStatus.WaitingForAnyPlayerToPlay.ToDescriptionString(), status.Status);
        }

        [Fact]
        public void Move_WhenPlayerOnePlayed_ReturnsWaitingForSecondPlayer()
        {
            var guid = _controller.Post(_playerOne);

            _controller.Join(guid.Value.ToString(), _playerTwo);

            var move = new GameMove()
            {
                Name = "Thomas",
                Move = PlayerMove.Rock
            };

            var result = _controller.Move(guid.Value.ToString(), move);

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.WaitingForSecondPlayerToPlay.ToDescriptionString(), result.Value.Status);
        }

       

        [Theory]
        [InlineData(PlayerMove.Rock, PlayerMove.Rock, PlayerStatus.Draw)]
        [InlineData(PlayerMove.Rock, PlayerMove.Paper, PlayerStatus.Loss)]
        [InlineData(PlayerMove.Rock, PlayerMove.Scissors, PlayerStatus.Win)]
        [InlineData(PlayerMove.Paper, PlayerMove.Rock, PlayerStatus.Win)]
        [InlineData(PlayerMove.Paper, PlayerMove.Paper, PlayerStatus.Draw)]
        [InlineData(PlayerMove.Paper, PlayerMove.Scissors, PlayerStatus.Loss)]
        [InlineData(PlayerMove.Scissors, PlayerMove.Rock, PlayerStatus.Loss)]
        [InlineData(PlayerMove.Scissors, PlayerMove.Paper, PlayerStatus.Win)]
        [InlineData(PlayerMove.Scissors, PlayerMove.Scissors, PlayerStatus.Draw)]
        public void Move_WhenGameIsFinished_ReturnsWinForPlayerOne(PlayerMove playerOneMove, PlayerMove playerTwoMove, PlayerStatus expectedStatus)
        {

            var guid = _controller.Post(_playerOne);

            _controller.Join(guid.Value.ToString(), _playerTwo);

            GameMove moveOne = new GameMove() {
                Name = _playerOne.Name,
                Move = playerOneMove
            };

            _controller.Move(guid.Value.ToString(), moveOne);

            GameMove moveTwo = new GameMove() {
                Name = _playerTwo.Name,
                Move = playerTwoMove
            };
            _controller.Move(guid.Value.ToString(), moveTwo);

            var result = _controller.Status(guid.Value.ToString());

            Assert.IsType<ActionResult<StatusModel>>(result);
            Assert.Equal(GameStatus.GameFinished.ToDescriptionString(), result.Value.Status);
            
            var firstPlayer = result.Value.Players.FirstOrDefault(p => p.Name == _playerOne.Name);
            Assert.NotNull(firstPlayer);
            Assert.Equal(expectedStatus, firstPlayer.GetStatus());

           
        }
    }
}
