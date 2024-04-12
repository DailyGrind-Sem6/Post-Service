namespace Post_Service.Entities;

public class PostDatabaseSettings : IPostDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PostsCollectionName { get; set; } = null!;
}