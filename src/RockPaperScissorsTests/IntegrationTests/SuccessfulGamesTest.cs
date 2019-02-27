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

        /// <summary>
        /// Gets as json async. Have to make this a bit diffrent due to sending a body as wall
        /// </summary>
        /// <returns>The as json async.</returns>
        /// <param name="url">URL.</param>
        /// <param name="model">Model.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<string> GetAsJsonAsync<T>(string url, T model)
        {
            //Assert.Equal("Testar", _client.BaseAddress.Port.ToString());
            // Create the request base
            var request = new HttpRequestMessage
            {
                RequestUri = url,
                Method = HttpMethod.Get,
            };

            // Add the content
            request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = _client.SendAsync(request).Result;
            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();

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
            stringResponse = await GetAsJsonAsync("/api/games/" + guid, _playerOne);
            var status = JsonConvert.DeserializeObject<StatusModel>(stringResponse);

            Assert.IsType<StatusModel>(status);
            Assert.Equal(PlayerStatus.Win, status.PlayerStatus);


        }
    }
}



