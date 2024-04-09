using System.ComponentModel.DataAnnotations;

namespace Post_Service.Entities;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
        
    public string UserId { get; set; }
}