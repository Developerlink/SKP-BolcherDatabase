using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using BolcherDBModelLibrary;
using Newtonsoft.Json;

namespace BolcherDBAPI.IntegrationTests.Controllers
{
    public class ColorsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public ColorsControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/colors");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetColors_ReturnsSuccessStatuscode()
        {
            var response = await _client.GetAsync("");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetColors_ReturnsExpectedResponse()
        {
            //var expected = JsonConvert.DeserializeObject<List<Color>>("[{ 'id':1,'name':'Sort','candies':[]},{ 'id':2,'name':'Grå','candies':[]},{ 'id':3,'name':'Hvid','candies':[]},{ 'id':4,'name':'Rød','candies':[]},{ 'id':5,'name':'Orange','candies':[]},{ 'id':6,'name':'Gul','candies':[]},{ 'id':7,'name':'Grøn','candies':[]},{ 'id':8,'name':'Blå','candies':[]},{ 'id':9,'name':'Lilla','candies':[]},{ 'id':10,'name':'Brun','candies':[]},{ 'id':11,'name':'Lyseblå','candies':[]}]");

            var actual = await _client.GetFromJsonAsync<List<Color>>("");


            // This asserts that any critical properties have expected values.
            Assert.NotNull(actual);
            //Assert.True(expected.SequenceEqual(actual));
            //var expected = 11;

            //Assert.Equal(expected, actual.Count);

            Assert.True(actual.Count > 0);
        }
    }
}
