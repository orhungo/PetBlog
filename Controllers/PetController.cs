using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Migrations;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class PetController:Controller {

    MyAppContext _context= new MyAppContext();
     
     [HttpGet]
      public IActionResult PetProfil() {
        return View();
    }

    [HttpPost]
    public IActionResult PetProfil(Pet pet, IFormFile photo) {
        if (ModelState.IsValid) {

             if (photo != null && photo.Length > 0)
        {
          
        using (var memoryStream = new MemoryStream())
        {
            photo.CopyTo(memoryStream);
            pet.petFoto = memoryStream.ToArray();
        }
          
            _context.petler.Add(pet);
            _context.SaveChanges();

             ViewData["SuccessMessage"] = "Kayıt işlemi başarılı!";
            return View(pet);
        }
    }
      return View (pet);
  }
}
