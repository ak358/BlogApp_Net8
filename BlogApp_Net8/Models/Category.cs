using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Category
    {
        public int Id { get; set; } // カテゴリの ID
        public string CategoryName { get; set; } // カテゴリの名前
        public int Count { get; set; } // カテゴリに含まれる記事の数

        // カテゴリに属する記事のコレクション
        [ValidateNever]

        public virtual ICollection<Article> Articles { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]

        public virtual User User { get; set; }

    }
}
