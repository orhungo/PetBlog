using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using PetBlog.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace petblog.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly MyAppContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public YoneticiController(MyAppContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(string kullaniciAdi, string sifre)
        {
            
            var tumYoneticiler = _context.Yoneticiler.ToList();
            
            var yonetici = _context.Yoneticiler.FirstOrDefault(x => 
                x.kullaniciAdi == kullaniciAdi && x.sifre == sifre);

            if (yonetici != null)
            {
                HttpContext.Session.SetInt32("YoneticiId", yonetici.yoneticiId);
                return RedirectToAction("Panel");
            }
            return View();  
        }

        public IActionResult Panel(string arama)
        {
            if (HttpContext.Session.GetInt32("YoneticiId") == null)
                return RedirectToAction("GirisYap");

            var bloglar = _context.Bloglar.AsQueryable();

            
            if (!string.IsNullOrEmpty(arama))
            {
                bloglar = bloglar.Where(b => b.baslik.Contains(arama) || b.icerik.Contains(arama));
            }

            return View(bloglar.ToList());
        }

        public IActionResult CikisYap()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("GirisYap");
        }

        
        public IActionResult BlogEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogEkle(Blog blog, IFormFile gorsel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    blog.olusturmaTarihi = DateTime.Now;
                    blog.yoneticiId = HttpContext.Session.GetInt32("YoneticiId") ?? 0;

                    // Görsel yükleme işlemi
                    if (gorsel != null)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blog-images");
                        Directory.CreateDirectory(uploadsFolder);

                        var fileName = Guid.NewGuid() + Path.GetExtension(gorsel.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await gorsel.CopyToAsync(fileStream);
                        }

                        blog.gorselUrl = "/uploads/blog-images/" + fileName; 
                    }

                    _context.Bloglar.Add(blog);
                    await _context.SaveChangesAsync();

                    TempData["Basari"] = "Blog başarıyla eklendi!";
                    return RedirectToAction("Panel");
                }
                catch (Exception ex)
                {
                    TempData["Hata"] = "Blog eklenirken bir hata oluştu: " + ex.Message;
                }
            }
            return View(blog);
        }

        public IActionResult BlogDuzenle(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> BlogDuzenle(Blog blog, IFormFile gorsel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mevcutBlog = _context.Bloglar.FirstOrDefault(b => b.blogId == blog.blogId);
                    if (mevcutBlog == null)
                    {
                        return NotFound();
                    }

   
                    mevcutBlog.baslik = blog.baslik;
                    mevcutBlog.icerik = blog.icerik;

                    // Görsel yükleme 
                    if (gorsel != null)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blog-images");
                        Directory.CreateDirectory(uploadsFolder);

                        var fileName = Guid.NewGuid() + Path.GetExtension(gorsel.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await gorsel.CopyToAsync(fileStream);
                        }

                        mevcutBlog.gorselUrl = "/uploads/blog-images/" + fileName; 
                    }

                    _context.SaveChanges();

                    TempData["Basari"] = "Blog başarıyla güncellendi!";
                    return RedirectToAction("Panel");
                }
                catch (Exception ex)
                {
                    TempData["Hata"] = "Blog güncellenirken bir hata oluştu: " + ex.Message;
                }
            }
            return View(blog);
        }

        public IActionResult BlogGoruntule(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound(); 
            }
            return View(blog); 
        }

        public IActionResult Blog(int id)
        {
            var blog = _context.Bloglar.FirstOrDefault(b => b.blogId == id);
            if (blog == null)
            {
                return NotFound(); 
            }
            return View(blog); 
        }

        [HttpPost]
        public async Task<IActionResult> DurumDegistir(int id)
        {
            var blog = await _context.Bloglar.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            //aktif pasif yapma
            blog.aktifMi = !blog.aktifMi; 
            _context.Bloglar.Update(blog);
            await _context.SaveChangesAsync();

            TempData["Basari"] = "Blog durumu başarıyla güncellendi!";
            return RedirectToAction("Panel");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // silme işlemi
            var blog = _context.Bloglar.Find(id);
            
            if (blog != null)
            {
                
                _context.Bloglar.Remove(blog);
                _context.SaveChanges(); 
                TempData["SuccessMessage"] = "Blog yazısı başarıyla silindi."; 
            }
            else
            {
                TempData["ErrorMessage"] = "Blog yazısı bulunamadı."; 
            }
            
            return RedirectToAction("Panel"); 
        }
    }
}