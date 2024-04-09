namespace Post_Service.Entities;

public interface IPostDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

    public string PostsCollectionName { get; set; }
}