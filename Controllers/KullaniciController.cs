using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class KullaniciController : Controller {

    MyAppContext _context= new MyAppContext();

      public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string email, string sifre)
    {
        // Kullanıcıyı veritabanında arama
        var Kullanici = _context.kayitli
            .FirstOrDefault(k => k.email == email && k.sifre == sifre);

        // Eğer kullanıcı bulunursa
        if (Kullanici != null)
        {
            // Kullanıcıyı başarılı bir şekilde giriş yaptıysa
            TempData["SuccessMessage"] = "Giriş başarılı!";
            return RedirectToAction("KullaniciProfil", "Profil"); // Profile yönlendir
        }
        else
        {
            // Kullanıcı bulunamazsa 
            TempData["ErrorMessage"] = "Geçersiz email veya şifre.";
            return View();
        }
    }
}