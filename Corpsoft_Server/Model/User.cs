﻿namespace Corpsoft_Server.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Post> Posts  { get; set; }
    }
}
