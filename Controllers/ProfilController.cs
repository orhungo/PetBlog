using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class ProfilController:Controller {

    MyAppContext _context= new MyAppContext();

      public IActionResult KullaniciProfil() {
        return View();
    }

}