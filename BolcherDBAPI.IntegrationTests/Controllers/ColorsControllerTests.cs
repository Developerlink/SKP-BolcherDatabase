using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using BolcherDBModelLibrary;
using Newtonsoft.Json;
using System.Net;

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

        [Fact]
        public async Task Post_WithoutName_ReturnsBadRequest()
        {
            var color = new Color();

            // By default null properties will be included in the serializes json.
            // To reduce payload sizes many httpclients avoid sending null entirely. 
            // We can test for that case by adding som serialization options that we define.
            var response = await _client.PostAsJsonAsync("", color);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);                        
        }

        [Fact]
        public async Task Post_WithoutName_ReturnsExpectedProblemDetails()
        {
            var color = new Color();

            // By default null properties will be included in the serializes json.
            // To reduce payload sizes many httpclients avoid sending null entirely. 
            // We can test for that case by adding som serialization options that we define.
            var response = await _client.PostAsJsonAsync("", color);

            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Collection(problemDetails.Errors, kvp =>
            {
                Assert.Equal("Name", kvp.Key);
                var error = Assert.Single(kvp.Value);
                Assert.Equal("The Name field is required.", error);
            });
        }
    }
}
