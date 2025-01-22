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
  
    var Kullanici = _context.kayitli.FirstOrDefault(k => k.email == email && k.sifre == sifre);

  
    if (Kullanici != null)
    {
       
        HttpContext.Session.SetInt32("userId", Kullanici.userID); 
        TempData["SuccessMessage"] = "Giriş başarılı!";
        return RedirectToAction("KullaniciProfil", "Profil"); 
    }
    else
    {

        TempData["ErrorMessage"] = "Geçersiz email veya şifre.";
        return View();
    }
}

}