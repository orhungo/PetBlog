using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using PetBlog.Models;

namespace petblog.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly MyAppContext _context;

        public YoneticiController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(string kullaniciAdi, string sifre)
        {
            // Debug için kontrol
            var tumYoneticiler = _context.Yoneticiler.ToList();
            
            var yonetici = _context.Yoneticiler.FirstOrDefault(x => 
                x.kullaniciAdi == kullaniciAdi && x.sifre == sifre);

            if (yonetici != null)
            {
                HttpContext.Session.SetInt32("YoneticiId", yonetici.yoneticiId);
                return RedirectToAction("Panel");
            }

            // Debug için detaylı hata mesajı
            ViewBag.Hata = $"Giriş başarısız. Girilen: {kullaniciAdi}/{sifre}";
            return View();
        }

        public IActionResult Panel()
        {
            if (HttpContext.Session.GetInt32("YoneticiId") == null)
                return RedirectToAction("GirisYap");

            var bloglar = _context.Bloglar.ToList();
            return View(bloglar);
        }

        public IActionResult CikisYap()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("GirisYap");
        }

        // Blog işlemleri için metodlar:
        public IActionResult BlogEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BlogEkle(Blog blog)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Gerekli alanları doldur
                    blog.olusturmaTarihi = DateTime.Now;
                    blog.yoneticiId = HttpContext.Session.GetInt32("YoneticiId") ?? 0;
                    
                    // Debug için
                    Console.WriteLine($"Blog başlık: {blog.baslik}");
                    Console.WriteLine($"Blog içerik: {blog.icerik}");
                    
                    // Veritabanına ekle
                    _context.Bloglar.Add(blog);
                    _context.SaveChanges();

                    TempData["Mesaj"] = "Blog başarıyla eklendi!";
                    return RedirectToAction("Panel");
                }
                catch (Exception ex)
                {
                    // Hata durumunda
                    ModelState.AddModelError("", "Blog eklenirken bir hata oluştu: " + ex.Message);
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }

            // Hata varsa formu tekrar göster
            return View(blog);
        }
    }
}