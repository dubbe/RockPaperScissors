using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RockPaperScissors;
using RockPaperScissors.Models;
using Xunit;

namespace RockPaperScissorsTests.IntegrationTests
{
    public class SuccessfulGamesTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private PlayerModel _playerOne;
        private PlayerModel _playerTwo;

        public SuccessfulGamesTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();

            _playerOne = new PlayerModel()
            {
                Name = "Thomas"
            };

            _playerTwo = new PlayerModel()
            {
                Name = "Sabine"
            };

        }

        public async Task<string> PostAsJsonAsync<T>(string url, T model)
        {
            var httpResponse = await _client.PostAsJsonAsync(url, model);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadAsStringAsync();
        }

    
        [Fact]
        public async Task SuccessfulGame()
        {

            // Create game
            var stringResponse = await PostAsJsonAsync("/api/games", _playerOne);
            var guid = JsonConvert.DeserializeObject<Guid>(stringResponse);
            Assert.IsType<Guid>(guid);

            // Joing with player two
            stringResponse = await PostAsJsonAsync("/api/games/" + guid + "/join", _playerTwo);
            // var joined = JsonConvert.DeserializeObject<Boolean>(stringResponse);
            // Assert.Equal("test", stringResponse);

            // Player one plays rock
            _playerOne.Move = PlayerMove.Rock;
            stringResponse = await PostAsJsonAsync("/api/games/" + guid + "/move", _playerOne);

            // Player one plays rock
            _playerTwo.Move = PlayerMove.Scissors;
            stringResponse = await PostAsJsonAsync("/api/games/" + guid + "/move", _playerTwo);

            // Player one checks status
            // stringResponse = await GetAsJsonAsync("/api/games/" + guid, _playerOne);
            // var status = JsonConvert.DeserializeObject<StatusModel>(stringResponse);

            // Assert.IsType<StatusModel>(status);
            // Assert.Equal(PlayerStatus.Win, status.PlayerStatus);


        }
    }
}



