using Corpsoft_Server.Model;

namespace Corpsoft_Server.Dto
{
    public class CommentDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
