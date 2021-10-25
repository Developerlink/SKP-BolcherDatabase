using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup> 
        where TStartup : class
    {
        public FakeCloudDatabase FakeCloudDatabase { get; }

        public CustomWebApplicationFactory()
        {
            FakeCloudDatabase = new FakeCloudDatabase();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<ICloudDatabase>(FakeCloudDatabase);
                });
        }
    }

    public class FakeCloudDatabase : ICloudDatabase
    {

        public void WithDefaultProducts()
        {

        }
    }

    public interface ICloudDatabase
    {
    }
}
