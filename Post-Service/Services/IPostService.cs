using MongoDB.Bson;
using Post_Service.Entities;

namespace Post_Service.Services;

public interface IPostService
{
    public Task<List<Post>> GetPosts();
    public Post GetPostById(string id);
    public Task<Post> CreatePost(Post post);
}