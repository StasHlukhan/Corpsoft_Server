using Corpsoft_Server.Data;
using Corpsoft_Server.Dto;
using Corpsoft_Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Corpsoft_Server.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get/posts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpPost("create/post")]

        public async Task<IActionResult> CreatePost([FromBody] PostDto postModel)
        {
            try
            {
                int userId = postModel.UserId;
                 var post = new Post
                 {
                     UserId = userId,
                     Title = postModel.Title,
                     Content = postModel.Content
                 };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return Ok("Пост був успішно створений");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Помилка створення поста: " + ex.Message);
            }
        }

        [HttpGet("post/${postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            try
            {
                var post = await _context.Posts
                    .Include(t => t.Comments)
                    .FirstOrDefaultAsync(t => t.PostId == postId);

                if (post == null)
                {
                    return NotFound($"Тест з ідентифікатором {postId} не знайдений");
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка: {ex.Message}");
            }
        }
    }
}
