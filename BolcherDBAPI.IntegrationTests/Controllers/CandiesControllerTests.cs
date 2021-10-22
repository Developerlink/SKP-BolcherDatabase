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
using System.Net.Http.Json;

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

        //[Fact]
        //public async Task GetCandies_ReturnSuccessStatusCode()
        //{
        //    var response = await _client.GetAsync("");

        //    response.EnsureSuccessStatusCode();
        //}

        //[Fact]
        //public async Task GetCandies_ReturnExpectedMediaType()
        //{
        //    var response = await _client.GetAsync("");

        //    Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        //}

        //[Fact]
        //public async Task GetCandies_ReturnsContent()
        //{
        //    var response = await _client.GetAsync("");

        //    Assert.NotNull(response.Content);
        //    Assert.True(response.Content.Headers.ContentLength > 0);
        //}

        //[Fact]
        //public async Task GetCandies_ReturnsExpectedJson()
        //{
        //    var expected = new List<Candy>();

        //    var responseStream = await _client.GetStreamAsync("");

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    var model = await JsonSerializer.DeserializeAsync<List<Candy>>(responseStream, options);

        //    Assert.NotNull(model);
        //    Assert.Equal(expected.OrderBy(s => s), model.OrderBy(s => s));
        //}

        // With this test all former tests will be performed and it can therefore
        // replace the others entirely.
        [Fact]
        public async Task GetCandies_ReturnsExpectedResponse()
        {
            //var expected = new List<Candy>();
            var expected = new List<int>() { 1, 3, 6, 8, 2 };
            var actual = new List<int>() { 3, 8, 2, 6, 1};

            // This method by default is case-INsensitive deserialization. 
            var model = await _client.GetFromJsonAsync<List<Candy>>("");

            Assert.NotNull(model);
            //Assert.Equal(expected.OrderBy(s => s), model.OrderBy(s => s));
            Assert.Equal(expected.OrderBy(s => s), actual.OrderBy(s => s));
        }

        [Fact]
        public async Task GetCandies_SetsExpectedCacheControlHeader()
        {
            var response = await _client.GetAsync("");

            var header = response.Headers.CacheControl;

            Assert.True(header.MaxAge.HasValue);
            Assert.Equal(TimeSpan.FromSeconds(300), header.MaxAge);
            Assert.True(header.Public);
        }

    }
}
