using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using petblog.Models;
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
            return RedirectToAction("KullaniciProfil", "Profil");
        }
    }
      return View (pet);
  }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      
        var pet = await _context.petler.FindAsync(id);
        if (pet == null)
        {
           
            return NotFound();
        }

        
        _context.petler.Remove(pet);
        await _context.SaveChangesAsync(); 

        
        return RedirectToAction("KullaniciProfil", "Profil");
    }


    [HttpGet]
    public IActionResult PetDuzenle(int id)
    {
        var pet = _context.petler.FirstOrDefault(p => p.petID == Convert.ToInt32(id));

        if (pet == null)
        {
            return NotFound(); 
        }

        return View(pet); 
    }

    [HttpPost]
[ValidateAntiForgeryToken]  
public IActionResult PetDuzenle(Pet pet, IFormFile petFoto)
{

    var existingPet = _context.petler.FirstOrDefault(p => p.petID == pet.petID);

    if (existingPet == null)
    {
        return NotFound(); 
    }


    if (petFoto != null && petFoto.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            petFoto.CopyTo(memoryStream);
            existingPet.petFoto = memoryStream.ToArray(); 
        }
    }


    existingPet.petAd = pet.petAd;
    existingPet.petYas = pet.petYas;
    existingPet.petBilgi = pet.petBilgi;


    _context.SaveChanges();

    return RedirectToAction("KullaniciProfil", "Profil");
}
}




