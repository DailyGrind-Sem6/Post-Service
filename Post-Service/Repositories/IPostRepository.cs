using Post_Service.Entities;

namespace Post_Service.Repositories;

public interface IPostRepository
{
    public Task<List<Post>> GetAll();
    public Task<Post> Get(string id);
    public Task<Post> Create(Post post);
    public Task Update(string id, Post postIn);
    public Task Remove(string id);
}