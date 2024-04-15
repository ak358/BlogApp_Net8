namespace BlogApp_Net8.Models
{
    public class Category
    {
        public int Id { get; set; } // カテゴリの ID
        public string CategoryName { get; set; } // カテゴリの名前
        public int Count { get; set; } // カテゴリに含まれる記事の数

        // カテゴリに属する記事のコレクション
        public ICollection<Article> Articles { get; set; }
    }
}
