using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Article
    {
        public int Id { get; set; } // 記事の ID
        public string Title { get; set; } // 記事のタイトル
        public string Body { get; set; } // 記事の本文
        public DateTime CreateDate { get; set; } // 記事の作成日時
        public DateTime UpdateDate { get; set; } // 記事の更新日時

        [NotMapped]
        public string CategoryName { get; set; } // 記事のカテゴリ名

        public virtual Category category{ get; set; }
        public virtual ICollection<Comment> comments { get; set; }
    }
}
