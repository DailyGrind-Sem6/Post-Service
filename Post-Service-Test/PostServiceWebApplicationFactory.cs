using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Mongo2Go;
using MongoDB.Driver;
using Post_Service.Repositories;

namespace Post_Service_Test;

public class PostServiceWebApplicationFactory : WebApplicationFactory<Program>
{
    private MongoDbRunner _runner;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing registration for IPostRepository
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IPostRepository));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Start MongoDbRunner
            _runner = MongoDbRunner.Start();

            // Register the MongoDB client with the connection string provided by MongoDbRunner
            services.AddSingleton<IMongoClient>(new MongoClient(_runner.ConnectionString));

            // Register the repository with the MongoDB client
            services.AddSingleton<IPostRepository, PostRepository>();
        });
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _runner.Dispose();
        }
    }
}