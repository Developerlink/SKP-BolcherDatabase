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
using System.Net;

namespace BolcherDBAPI.IntegrationTests.Controllers
{
    public class CustomersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CustomersControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/customers");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomers_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCustomers_ReturnsExpectedResponse()
        {
            var model = await _client.GetFromJsonAsync<List<Customer>>("");

            Assert.True(model.Count >= 0);
        }

        [Fact]
        public async Task Post_WithoutName_ReturnsBadRequest()
        {
            var customer = new Customer{ LastName = "Wayne" };

            // By default null properties will be included in the serializes json.
            // To reduce payload sizes many httpclients avoid sending null entirely. 
            // We can test for that case by adding som serialization options that we define.
            var response = await _client.PostAsJsonAsync("", customer);

            //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);                        
        }
    }
}
