using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Post_Service_Test
{
    public class Tests
    {
        private WebApplicationFactory<Program> _application;
        private HttpClient _client;
        
        [SetUp]
        public void Setup()
        {
            _application = new PostServiceWebApplicationFactory();
            _client = _application.CreateClient();
        }
        
        [TearDown]
        public void TearDown()
        {
            _application?.Dispose();
            _client?.Dispose();
        }

        [Test]
        public async Task Get_Returns_Posts()
        {
            var response = await _client.GetAsync("/api/posts");
    
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(response.Content.ReadAsStringAsync().Result, Is.EqualTo("{\"message\":\"bzbz \\uD83D\\uDC1D\"}"));
            });
        }
    }
}