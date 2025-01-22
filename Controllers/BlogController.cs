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
            var bloglar = _context.Bloglar.ToList(); // Tüm blogları al
            return View(bloglar); // Blogları görüntüleme sayfasına gönder
        }

        // Belirli bir blog yazısını görüntüleme
        public IActionResult Details(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound(); // Blog bulunamazsa 404 döner
            }
            return View(blog); // Blog verisini görüntüleme sayfasına gönderir
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
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Bloglar.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            _context.Bloglar.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
} 