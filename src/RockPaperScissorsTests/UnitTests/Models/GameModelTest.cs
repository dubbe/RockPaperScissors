using System;
using RockPaperScissors.Models;
using Xunit;

namespace RockPaperScissorsTests.UnitTests.Models
{
    public class GameModelTest
    {

        private PlayerModel _playerOne;
        private PlayerModel _playerTwo;
        private PlayerModel _playerThree;

        public GameModelTest()
        {
            _playerOne = new PlayerModel()
            {
                Name = "Thomas"
            };

            _playerTwo = new PlayerModel()
            {
                Name = "Sabine"
            };

            _playerThree = new PlayerModel()
            {
                Name = "Benjamin"
            };
        }

        [Fact]
        public void GameModel_WhenInitialized_IdIsGuid()
        {
            var game = new GameModel(_playerOne);
            Assert.IsType<Guid>(game.Id);
        }

        [Fact]
        public void GameModel_WhenCalledWithEmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new GameModel(new PlayerModel()));
        }

        [Fact]
        public void GameModel_WhenInitializedAndJoinedBySamePlatyer_ReturnsFalse()
        {
            var game = new GameModel(_playerOne);
            var joined = game.JoinGame(_playerOne);

            Assert.False(joined);
        }

        [Fact]
        public void JoinGame_WhenCalledOnce_ReturnsTrue()
        {
            var game = new GameModel(_playerOne);
            var joined = game.JoinGame(_playerTwo);

            Assert.True(joined);
        }

        [Fact]
        public void JoinGame_WhenCalledTwicwe_ReturnsFalse()
        {
            var game = new GameModel(_playerOne);

            // Join first time
            game.JoinGame(_playerTwo);

            // player three tries to join
            var joined = game.JoinGame(_playerThree);

            Assert.False(joined);
        }
    }

   
}