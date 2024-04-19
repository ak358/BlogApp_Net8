using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlogApp_Net8.Models;
using Microsoft.AspNetCore.Authorization;
using BlogApp_Net8.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApp_Net8.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(BlogDbContext context, 
            IHttpContextAccessor httpContextAccessor,
            ILogger<HomeController> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            var articles = new List<Article>();
            var categories = new List<Category>();

            //すべての記事を取得
            articles = await _context.Articles
                .Include(a => a.User)
                .Include(a => a.Category)
                .Include(a => a.Comments)
                .ToListAsync();

            //すべてのカテゴリーを取得
            categories = await _context.Categories
                .Include(a => a.User)
                .ToListAsync();

            List<string> categoryNames = categories.Select(c => c.CategoryName).Distinct().ToList();
            List<string> dates = articles.Select(a => a.CreateDate.ToString("yyyy/M")).Distinct().ToList();

            var model = new HomeViewModel()
            {
                Articles = articles,
                CategoryNames = categoryNames,
                Dates = dates
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Mypage()
        {
            return View();
        }

    }
}
