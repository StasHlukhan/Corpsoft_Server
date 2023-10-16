using Corpsoft_Server.Model;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Corpsoft_Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.Post)
                .HasForeignKey(t => t.PostId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
        
    }
}
