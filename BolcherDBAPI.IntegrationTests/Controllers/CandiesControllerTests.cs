using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BolcherDBModelLibrary;
using System.Text.Json;

namespace BolcherDBAPI.IntegrationTests.Controllers
{
    public class CandiesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public CandiesControllerTests(WebApplicationFactory<Startup> factory)
        {
            //_client = factory.CreateDefaultClient(new Uri("http://localhost/api/candies"));

            //_client = factory.CreateClient(new WebApplicationFactoryClientOptions
            //{
            //    BaseAddress = new Uri("http://localhost/api/candies")
            //});
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/candies");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCandies_ReturnSuccessStatusCode()
        {
            var response = await _client.GetAsync("");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCandies_ReturnExpectedMediaType()
        {
            var response = await _client.GetAsync("");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetCandies_ReturnsContent()
        {
            var response = await _client.GetAsync("");

            Assert.NotNull(response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetCandies_ReturnsExpectedJson()
        {
            var expected = new List<Candy>();

            var responseStream = await _client.GetStreamAsync("");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var model = await JsonSerializer.DeserializeAsync<List<Candy>>(responseStream, options);

            Assert.Empty(model);
            Assert.Equal(expected.Count, model.Count);
        }

    }
}
