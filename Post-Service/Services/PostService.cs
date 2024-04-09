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
}