using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApp_Net8.Data;
using BlogApp_Net8.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BlogApp_Net8.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticlesController(BlogDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            // ログイン中のユーザーのIDを取得
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //int userId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier));

            // userId を使用して記事を取得するクエリを構築
            var articles = await _context.Articles
                .Include(a => a.User)
                .Include(a => a.Category)
                .Include(a => a.Comments)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            // 取得した記事の中からカテゴリ名を取得する
            foreach (var categoryName in articles)
            {
                // カテゴリ名を設定
                categoryName.CategoryName = categoryName.Category?.CategoryName;
            }

            // 記事のリストをビューに渡して返す
            return View(articles);
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.User)
                .Include(a => a.Category)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (article == null)
            {
                return NotFound();
            }

            // カテゴリ名を設定
            article.CategoryName = article.Category?.CategoryName;
            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            // ログイン中のユーザーのIDを取得
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var categories = _context.Categories.Where(c => c.UserId == userId);
            ViewData["CategoryName"] = new SelectList(categories, "CategoryName", "CategoryName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Body,CreateDate,CategoryName,UserId")] Article article)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Where(u => u.Id == article.UserId).FirstOrDefaultAsync();
                List<Comment> comments = new();
                Category category = await _context.Categories.Include(c => c.User).
                                        Where(c => c.CategoryName == article.CategoryName && c.UserId == article.UserId).FirstOrDefaultAsync();
                article.User = user;
                article.Comments = comments;
                article.Category = category;

                _context.Add(article);
                await _context.SaveChangesAsync();
                await UpdateCategoryCount();

                return RedirectToAction(nameof(Index));
            }
            // ログイン中のユーザーのIDを取得
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var categories = _context.Categories.Where(c => c.UserId == userId);
            ViewData["CategoryName"] = new SelectList(categories, "CategoryName", "CategoryName");
            return View(article);
        }

        // GET: Articles/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            // ログイン中のユーザーのIDを取得
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var categories = _context.Categories.Where(c => c.UserId == userId);
            ViewData["CategoryName"] = new SelectList(categories, "CategoryName", "CategoryName");
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,CreateDate,UpdateDate,UserId,CategoryName")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    article.User = await _context.Users.Where(a => a.Id == article.UserId).FirstOrDefaultAsync();
                    article.Comments = await _context.Comments.Where(c => c.ArticleId == article.Id).ToListAsync();
                    article.Category = await _context.Categories.Where(c => c.CategoryName == article.CategoryName).FirstOrDefaultAsync();

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                    await UpdateCategoryCount();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // ログイン中のユーザーのIDを取得
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var categories = _context.Categories.Where(c => c.UserId == userId);
            ViewData["CategoryName"] = new SelectList(categories, "CategoryName", "CategoryName");
            return View(article);
        }

        // GET: Articles/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.User)
                .Include(a => a.Category)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (article == null)
            {
                return NotFound();
            }

            // カテゴリ名を設定
            article.CategoryName = article.Category?.CategoryName;

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            await UpdateCategoryCount();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }

        private async Task UpdateCategoryCount()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var categories = await _context.Categories
                                        .Include(c => c.User)
                                        .Where(c => c.UserId == userId)
                                        .ToListAsync();

            foreach (var category in categories)
            {
                // カテゴリの記事数を取得
                var count = _context.Articles.Count(a =>
                    a.Category.Id == category.Id && a.UserId == category.UserId);

                // カテゴリの記事数が現在の記事数と異なる場合のみ更新
                if (category.Count != count)
                {
                    category.Count = count;
                    _context.Categories.Update(category); // カテゴリの記事数を更新
                }
            }

            await _context.SaveChangesAsync(); // 変更をデータベースに保存

        }
    }
}
