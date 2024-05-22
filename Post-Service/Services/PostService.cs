using MongoDB.Bson;
using Post_Service.Entities;
using Post_Service.Repositories;

namespace Post_Service.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;

    public PostService(IPostRepository repository)
    {
        _repository = repository;
    }
    public Task<List<Post>> GetPosts()
    {
        return _repository.GetAll();
    }

    public Post GetPostById(string id)
    {
        var post = _repository.Get(id).Result;
        if (post == null)
        {
            throw new Exception("Post not found");
        }

        return post;
    }

    public Task<Post> CreatePost(Post post)
    {
        return _repository.Create(post);
    }
}