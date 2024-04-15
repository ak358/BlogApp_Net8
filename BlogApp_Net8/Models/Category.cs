namespace BlogApp_Net8.Models
{
    public class Category
    {
        public int Id { get; set; } // �J�e�S���� ID
        public string CategoryName { get; set; } // �J�e�S���̖��O
        public int Count { get; set; } // �J�e�S���Ɋ܂܂��L���̐�

        // �J�e�S���ɑ�����L���̃R���N�V����
        public ICollection<Article> Articles { get; set; }
    }
}
