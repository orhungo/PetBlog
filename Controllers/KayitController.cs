/*using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class KayitController : Controller {
    MyAppContext _context = new MyAppContext();

    
      public IActionResult KullaniciKayit()
    {
        return View();
    }

    [HttpPost]
    public IActionResult KullaniciKayit(Kayit kayit) {
        if (ModelState.IsValid) {
            _context.kayitli.Add(kayit);
            _context.SaveChanges();
             ViewData["SuccessMessage"] = "Kayıt işlemi başarılı!";
            return View(kayit);
        }
        return View (kayit);
    }


}*/