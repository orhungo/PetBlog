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

    public async Task<IActionResult> UploadPhoto(IFormFile file)
{
    if (file != null && file.Length > 0)
    {
        // Fotoğrafı byte dizisine çeviriyoruz
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var photoBytes = memoryStream.ToArray();

            // Fotoğrafı veritabanına kaydediyoruz
            var pet = new Pet
            {
                petAd = "Kedim",
                petBilgi = "Kedimin bilgisi.",
                petFoto = photoBytes // Fotoğrafı byte dizisi olarak kaydediyoruz
            };

            _context.petler.Add(pet);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Profile");
    }

    return View();
}


  


}