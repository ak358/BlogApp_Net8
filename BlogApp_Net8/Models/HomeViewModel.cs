using Microsoft.AspNetCore.Mvc;

namespace BlogApp_Net8.Models
{

    public class HomeViewModel
    {
        public List<Article> Articles { get; set; }
        public List<string> CategoryNames { get; set; }
        public List<string> Dates { get; set; }
    }

}
