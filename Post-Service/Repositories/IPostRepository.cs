using MongoDB.Bson;
using Post_Service.Entities;

namespace Post_Service.Repositories;

public interface IPostRepository
{
    public Task<List<Post>> GetAll();
    public Task<Post> Get(ObjectId id);
    public Task<Post> Create(Post post);
    public Task Update(ObjectId id, Post postIn);
    public Task Remove(ObjectId id);
}