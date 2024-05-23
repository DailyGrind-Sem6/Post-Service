using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Post_Service.Entities;
using Post_Service.Services;

namespace Post_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostService _service;

        public PostsController(IPostService service, ILogger<PostsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine("Getting data from MongoDB...");
            var posts = await _service.GetPosts();
            
            return Ok(posts);
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var post = _service.GetPostById(id);
                
                return Ok(post);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            Console.WriteLine("Creating post...");
            var createdPost = await _service.CreatePost(post);
            return Ok(createdPost);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] Post post)
        {
            await _service.UpdatePost(id, post);
            return Ok();
        }

        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemovePost(string id)
        {
            await _service.RemovePost(id);
            return Ok();
        }
    }
}
