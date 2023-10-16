namespace Corpsoft_Server.Model
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public List<Comment> Comments   { get; set; }

    }
}
