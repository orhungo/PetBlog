using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class PetController:Controller {

    MyAppContext _context= new MyAppContext();

      public IActionResult PetProfil() {
        return View();
    }
}