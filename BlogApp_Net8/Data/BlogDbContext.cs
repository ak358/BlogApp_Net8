using Microsoft.EntityFrameworkCore;
using BlogApp_Net8.Models;

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

            modelBuilder.Entity<Article>(article =>
            {
                // カテゴリとの関係を定義
                article.HasOne(a => a.Category)
                       .WithMany(c => c.Articles);  // カテゴリが複数の記事を持つ

                // コメントとの関係を定義
                article.HasMany(a => a.Comments)  // 記事が複数のコメントを持つ
                       .WithOne(c => c.Article)  // コメントは1つの記事に属する
                       .HasForeignKey(c => c.ArticleId);  // コメントの外部キーとして記事の ID を使用
            });

            modelBuilder.Entity<Comment>(comment =>
            {
                // 記事との関係を定義
                comment.HasOne(c => c.Article)
                       .WithMany(a => a.Comments)  // 記事が複数のコメントを持つ
                       .HasForeignKey(c => c.ArticleId);  // コメントの外部キーとして記事の ID を使用
            });
        }
    }
}
