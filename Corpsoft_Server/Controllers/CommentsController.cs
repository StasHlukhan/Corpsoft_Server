using Corpsoft_Server.Data;
using Corpsoft_Server.Dto;
using Corpsoft_Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Corpsoft_Server.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("post/comments")]

        public async Task<IActionResult> AddCommentToPost( [FromBody] CommentDto commentModel)
        {
            var postId = commentModel.PostId;
            try
            {
                var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == postId);
                     if (post == null)
                {
                    return NotFound($"Тест з ідентифікатором {postId} не знайдений");
                }
                var comment = new Comment
                {
                    Content = commentModel.Content,
                    UserId = commentModel.UserId,
                    PostId = postId,
                };
                comment.Post = post;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return Ok($"Питання було успішно додано до тесту {post.Title}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка: {ex.Message}");
            }
        }
       

    }
}
