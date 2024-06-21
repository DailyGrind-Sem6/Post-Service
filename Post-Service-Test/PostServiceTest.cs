using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Post_Service.Entities;

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
        public async Task Post_Returns_Created_Post()
        {
            // Arrange
            var newPost = TestData.singlePost();
            var createResponse = await _client.PostAsJsonAsync("/api/posts", newPost);
            createResponse.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/posts");

            // Act
            var posts = await response.Content.ReadFromJsonAsync<List<Post>>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(posts, Is.Not.Null);
                Assert.That(posts.Count, Is.GreaterThan(0));
                Assert.That(posts.Any(p => p.Title == newPost.Title && p.Content == newPost.Content && p.UserId == newPost.UserId));
            });
        }

        [Test]
        public async Task Get_Returns_Posts()
        {
            // Arrange
            var testPosts = TestData.multiplePosts();
            foreach (var post in testPosts)
            {
                var createResponse = await _client.PostAsJsonAsync("/api/posts", post);
                createResponse.EnsureSuccessStatusCode();
            }
            
            var response = await _client.GetAsync("/api/posts");
            // Act
            var posts = await response.Content.ReadFromJsonAsync<List<Post>>();
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(posts, Is.Not.Null);
                Assert.That(posts.Count, Is.GreaterThan(0));
            });
        }
        
        [Test]
        public async Task GetById_Returns_Correct_Post()
        {
            // Arrange
            var newPost = TestData.singlePost();
            var createResponse = await _client.PostAsJsonAsync("/api/posts", newPost);
            createResponse.EnsureSuccessStatusCode();
            var createdPost = await createResponse.Content.ReadFromJsonAsync<Post>();

            // Act
            var response = await _client.GetAsync($"/api/posts/{createdPost.Id}");

            var content = await response.Content.ReadAsStringAsync();
    
            var retrievedPost = await response.Content.ReadFromJsonAsync<Post>();

            Console.WriteLine("Retrieved post ID: " + retrievedPost.Id);
            Console.WriteLine("Created post ID: " + createdPost.Id);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(retrievedPost, Is.Not.Null);
                Assert.That(retrievedPost.Id, Is.EqualTo(createdPost.Id));
            });
        }
        
        [Test]
        public async Task Remove_Deletes_Post()
        {
            // Arrange
            var newPost = TestData.singlePost();
            var createResponse = await _client.PostAsJsonAsync("/api/posts", newPost);
            createResponse.EnsureSuccessStatusCode();
            var createdPost = await createResponse.Content.ReadFromJsonAsync<Post>();

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/posts/{createdPost.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/posts/{createdPost.Id}");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }

        [Test]
        public async Task Update_Updates_Post()
        {
            // Arrange
            var newPost = TestData.singlePost();
            var createResponse = await _client.PostAsJsonAsync("/api/posts", newPost);
            createResponse.EnsureSuccessStatusCode();
            var createdPost = await createResponse.Content.ReadFromJsonAsync<Post>();

            createdPost.Title = "Updated Title";
            createdPost.Content = "Updated Content";

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/api/posts/{createdPost.Id}", createdPost);
            updateResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/posts/{createdPost.Id}");
            var updatedPost = await getResponse.Content.ReadFromJsonAsync<Post>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(updatedPost.Title, Is.EqualTo("Updated Title"));
                Assert.That(updatedPost.Content, Is.EqualTo("Updated Content"));
            });
        }
    }
}