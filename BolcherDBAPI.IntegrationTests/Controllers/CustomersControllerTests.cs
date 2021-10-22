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
    }
}
