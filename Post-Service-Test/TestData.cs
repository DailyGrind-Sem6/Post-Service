using Post_Service.Entities;
using System;

namespace Post_Service_Test
{
    public static class TestData
    {
        public static Post singlePost()
        {
            return new Post()
            {
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
                    Title = Guid.NewGuid().ToString(),
                    Content = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = Guid.NewGuid().ToString()
                },
                new Post()
                {
                    Title = Guid.NewGuid().ToString(),
                    Content = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = Guid.NewGuid().ToString()
                },
                new Post()
                {
                    Title = Guid.NewGuid().ToString(),
                    Content = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = Guid.NewGuid().ToString()
                }
            };
        }
    }
}