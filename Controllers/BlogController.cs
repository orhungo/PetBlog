using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;
using System.Linq;

namespace petblog.Controllers
{
    public class BlogController : Controller
    {
        private readonly MyAppContext _context;

        public BlogController(MyAppContext context)
        {
            _context = context;
        }

        // Blog yazılarını listeleme
        public IActionResult Index()
        {
            var bloglar = _context.Bloglar.ToList(); 
            return View(bloglar); 
        }
        
                public IActionResult Details(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog); // Sadece mevcut blog verisini görüntüleme sayfasına gönderir
        }

        // Blog ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        // Blog ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Bloglar.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // Blog düzenleme sayfası
        public IActionResult Edit(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // Blog düzenleme işlemi
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Bloglar.Update(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // Blog silme işlemi
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Veritabanından blog yazısını bul
            var blog = _context.Bloglar.Find(id);
            
            if (blog != null)
            {
                // Blog yazısını sil
                _context.Bloglar.Remove(blog);
                _context.SaveChanges(); // Değişiklikleri kaydet
                TempData["SuccessMessage"] = "Blog yazısı başarıyla silindi."; // Başarılı mesajı
            }
            else
            {
                TempData["ErrorMessage"] = "Blog yazısı bulunamadı."; // Hata mesajı
            }
            
            return RedirectToAction("Panel"); // Yönetici paneline yönlendir
        }
    }
} 