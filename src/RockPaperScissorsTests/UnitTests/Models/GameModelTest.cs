using System;
using RockPaperScissors.Controllers;
using RockPaperScissors.Models;
using Xunit;

namespace RockPaperScissorsTests.UnitTests.Models
{
    public class GameModelTest
    {

        private Player _playerOne;
        private Player _playerTwo;
        private Player _playerThree;

        public GameModelTest()
        {
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
        public void GameModel_WhenInitialized_IdIsGuid()
        {
            var game = new GameModel(_playerOne.Name);
            Assert.IsType<Guid>(game.Id);
        }

        [Fact]
        public void GameModel_WhenCalledWithEmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new GameModel(""));
        }

        [Fact]
        public void GameModel_WhenInitializedAndJoinedBySamePlatyer_ReturnsFalse()
        {
            var game = new GameModel(_playerOne.Name);
            var joined = game.JoinGame(_playerOne.Name);

            Assert.False(joined);
        }

        [Fact]
        public void JoinGame_WhenCalledOnce_ReturnsTrue()
        {
            var game = new GameModel(_playerOne.Name);
            var joined = game.JoinGame(_playerTwo.Name);

            Assert.True(joined);
        }

        [Fact]
        public void JoinGame_WhenCalledTwicwe_ReturnsFalse()
        {
            var game = new GameModel(_playerOne.Name);

            // Join first time
            game.JoinGame(_playerTwo.Name);

            // player three tries to join
            var joined = game.JoinGame(_playerThree.Name);

            Assert.False(joined);
        }
    }

   
}