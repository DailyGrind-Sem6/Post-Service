using Microsoft.AspNetCore.Mvc;

namespace Post_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "bzbz 🐝" });
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = "Post: " + id});
        }
    }
}
