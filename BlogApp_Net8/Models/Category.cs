using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Category
    {
        public int Id { get; set; } // �J�e�S���� ID
        public string CategoryName { get; set; } // �J�e�S���̖��O
        public int Count { get; set; } // �J�e�S���Ɋ܂܂��L���̐�

        // �J�e�S���ɑ�����L���̃R���N�V����
        [ValidateNever]

        public virtual ICollection<Article> Articles { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]

        public virtual User User { get; set; }

    }
}
