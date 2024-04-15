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

        public virtual Category category{ get; set; }
        public virtual ICollection<Comment> comments { get; set; }
    }
}
