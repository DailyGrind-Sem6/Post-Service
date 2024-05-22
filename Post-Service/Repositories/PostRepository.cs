using Microsoft.AspNetCore.Components.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Post_Service.Entities;

namespace Post_Service.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IMongoCollection<Post> _posts;

    public PostRepository(IPostDatabaseSettings settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.DatabaseName);
        _posts = database.GetCollection<Post>(settings.PostsCollectionName);
    }
    
    public async Task<List<Post>> GetAll()
    {
        return await _posts.Find(post => true).ToListAsync();
    }
    
    public Task<Post> Get(string id)
    {
        var filter = Builders<Post>.Filter.Eq(post => post.Id, id);
        var result = _posts.Find(filter).SingleOrDefaultAsync();

        return result;
    }
    
    public async Task<Post> Create(Post post)
    {
        await _posts.InsertOneAsync(post);
        return post;
    }
    
    public async Task Update(string id, Post postIn)
    {
        await _posts.ReplaceOneAsync(post => post.Id == id, postIn);
    }
    
    public async Task Remove(string id)
    {
        await _posts.DeleteOneAsync(post => post.Id == id);
    }
}