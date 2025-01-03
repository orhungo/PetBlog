using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;


namespace PetBlog.Controllers;

public class ProfilController:Controller {

    MyAppContext _context= new MyAppContext();

    [HttpGet]
      public IActionResult KullaniciProfil() {
         var pets = _context.petler.ToList();
         return View(pets);
    }

}