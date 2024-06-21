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

    public async Task UpdatePost(string id, Post post)
    {
        var existingPost = await _repository.Get(id);

        if (existingPost == null)
        {
            throw new Exception("Post not found");
        }
        
        post.Id = existingPost.Id;
        post.UserId = existingPost.UserId;
        post.CommentCount = existingPost.CommentCount;

        await _repository.Update(id, post);
    }

    public Task RemovePost(string id)
    {
        return _repository.Remove(id);
    }
    
    public Task IncrementCommentCount(string postId)
    {
        return _repository.IncrementCommentCount(postId);
    }
    
    public Task DecrementCommentCount(string postId)
    {
        return _repository.DecrementCommentCount(postId);
    }
}