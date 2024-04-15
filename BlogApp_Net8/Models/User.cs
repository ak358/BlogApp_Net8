﻿namespace BlogApp_Net8.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } // ユーザー名
        public string Password { get; set; } // パスワード

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
