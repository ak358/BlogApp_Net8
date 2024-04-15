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

                // 記事は1人のユーザーによって作成される
                article.HasOne(a => a.User)
                       .WithMany(u => u.Articles)
                       .HasForeignKey(a => a.UserId)
                       .OnDelete(DeleteBehavior.NoAction); // 削除操作の制約を指定

            });

            modelBuilder.Entity<Comment>(comment =>
            {
                // コメントは1人のユーザーによって作成される
                comment.HasOne(c => c.User)
                       .WithMany(u => u.Comments)
                       .HasForeignKey(c => c.UserId)
                       .OnDelete(DeleteBehavior.NoAction); // 削除操作の制約を指定
            });

        }
    }
}
