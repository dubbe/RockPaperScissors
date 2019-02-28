using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RockPaperScissors;
using RockPaperScissors.Controllers;
using RockPaperScissors.Models;
using Xunit;

namespace RockPaperScissorsTests.IntegrationTests
{
    public class SuccessfulGamesTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private Player _playerOne;
        private Player _playerTwo;

        public SuccessfulGamesTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();

            _playerOne = new Player()
            {
                Name = "Thomas"
            };

            _playerTwo = new Player()
            {
                Name = "Sabine"
            };

        }

        public async Task<dynamic> PostAsJsonAsync<T>(string url, T model)
        {
            var httpResponse = await _client.PostAsJsonAsync(url, model);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            string content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(content);
        }

        public async Task<dynamic> GetAsJsonAsync(string url)
        {
            var httpResponse = await _client.GetAsync(url);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            string content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(content);
        }

    
        [Fact]
        public async Task SuccessfulGame()
        {

            // Create game
            var createGameStringResponse = await PostAsJsonAsync("/api/games", _playerOne);
            string id = createGameStringResponse as string;

            // Joing with player two
            var joinGameStringResponse = await PostAsJsonAsync("/api/games/" + id + "/join", _playerTwo);

            // Player one plays rock
            var playerOneMoveStringResponse = await PostAsJsonAsync("/api/games/" + id + "/move", new GameMove(){
                Name = _playerOne.Name,
                Move = PlayerMove.Rock
            });

            // Player one checks status
            var playerOneChecksStatusBeforeGameIsFinishedStatus = await GetAsJsonAsync("/api/games/" + id) as JObject;
            Assert.Equal("Has made a move", playerOneChecksStatusBeforeGameIsFinishedStatus["players"][0]["status"].ToString());

            // Player two plays rock
            var stringResponse = await PostAsJsonAsync("/api/games/" + id + "/move", new GameMove(){
                Name = _playerTwo.Name,
                Move = PlayerMove.Scissors
            });

            // Player one checks status
            var playerOneChecksStatus = await GetAsJsonAsync("/api/games/" + id) as JObject;
            Assert.Equal("Played Rock and won the game", playerOneChecksStatus["players"][0]["status"].ToString());

            // Player two checks status
            var playerTwoChecksStatus = await GetAsJsonAsync("/api/games/" + id) as JObject;
            Assert.Equal("Played Scissors and lost the game", playerTwoChecksStatus["players"][1]["status"].ToString());


        }
    }
}



