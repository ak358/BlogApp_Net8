using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Comment
    {
        public int Id { get; set; } // コメントの ID
        public string Body { get; set; } // コメントの本文
        public DateTime CreateDate { get; set; } // コメントの作成日時

        // コメントが属する記事を表すナビゲーションプロパティ
        [ForeignKey("ArticleId")]
        [ValidateNever]

        public virtual Article Article { get; set; }

        // コメントが属する記事の ID を表す外部キー
        [NotMapped]
        public int ArticleId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]

        public virtual User User { get; set; }
    }
}
