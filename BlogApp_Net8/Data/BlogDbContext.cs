using Microsoft.EntityFrameworkCore;
using BlogApp_Net8.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BlogApp_Net8.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
