using MongoDB.Bson;
using Post_Service.Entities;

namespace Post_Service_Test;

public static class TestData
{
    public static Post singlePost()
    {
        return new Post()
        {
            Id = ObjectId.GenerateNewId(),
            Title = Guid.NewGuid().ToString(),
            Content = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            UserId = Guid.NewGuid().ToString()
        };
    }
    
    public static List<Post> multiplePosts()
    {
        return new List<Post>()
        {
            new Post()
            {
                Id = ObjectId.GenerateNewId(),
                Title = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Guid.NewGuid().ToString()
            },
            new Post()
            {
                Id = ObjectId.GenerateNewId(),
                Title = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Guid.NewGuid().ToString()
            },
            new Post()
            {
                Id = ObjectId.GenerateNewId(),
                Title = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Guid.NewGuid().ToString()
            }
        };
    }
}