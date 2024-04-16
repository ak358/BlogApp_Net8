using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Article
    {
        public int Id { get; set; } // �L���� ID
        public string Title { get; set; } // �L���̃^�C�g��
        public string Body { get; set; } // �L���̖{��
        public DateTime CreateDate { get; set; } // �L���̍쐬����
        public DateTime UpdateDate { get; set; } // �L���̍X�V����

        [NotMapped]
        public string CategoryName { get; set; } // �L���̃J�e�S����

        [ValidateNever]
        public virtual Category Category { get; set; }
        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual User User { get; set; }
    }
}
