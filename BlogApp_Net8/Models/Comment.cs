using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp_Net8.Models
{
    public class Comment
    {
        public int Id { get; set; } // �R�����g�� ID
        public string Body { get; set; } // �R�����g�̖{��
        public DateTime CreateDate { get; set; } // �R�����g�̍쐬����

        // �R�����g��������L����\���i�r�Q�[�V�����v���p�e�B
        public virtual Article Article { get; set; }

        // �R�����g��������L���� ID ��\���O���L�[
        [NotMapped]
        public int ArticleId { get; set; }
    }
}
