using AspNetCoreGeneratedDocument;
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
            return RedirectToAction("KullaniciProfil", "Profil");
        }
    }
      return View (pet);
  }

  [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        // İlgili pet'i veritabanından bul
        var pet = await _context.petler.FindAsync(id);
        if (pet == null)
        {
            // Eğer pet bulunamazsa, hata döndürüyoruz
            return NotFound();
        }

        // Pet'i veritabanından sil
        _context.petler.Remove(pet);
        await _context.SaveChangesAsync(); // Veritabanına değişikliği kaydet

        // Silme işlemi başarılı olduktan sonra profil sayfasına yönlendir
        return RedirectToAction("KullaniciProfil", "Profil");
    }


    [HttpGet]
    public IActionResult PetDuzenle(int id)
    {
        // İlgili pet'i veritabanından al
        var pet = _context.petler.FirstOrDefault(p => p.petID == id);

        if (pet == null)
        {
            return NotFound(); // Eğer pet bulunamazsa 404 döner
        }

        return View(pet); // Pet bilgisini düzenleme formuna gönder
    }

    [HttpPost]
[ValidateAntiForgeryToken]  // CSRF koruması
public IActionResult PetDuzenle(Pet pet, IFormFile petFoto)
{
    // Veritabanından ilgili pet'i bul
    var existingPet = _context.petler.FirstOrDefault(p => p.petID == pet.petID);

    if (existingPet == null)
    {
        return NotFound(); // Eğer pet bulunamazsa 404 döner
    }

    // Fotoğraf yüklenmişse
    if (petFoto != null && petFoto.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            petFoto.CopyTo(memoryStream); // Fotoğrafı belleğe yükle
            existingPet.petFoto = memoryStream.ToArray(); // Fotoğrafı byte array olarak kaydet
        }
    }

    // Diğer bilgileri güncelle
    existingPet.petAd = pet.petAd;
    existingPet.petYas = pet.petYas;
    existingPet.petBilgi = pet.petBilgi;

    // Değişiklikleri kaydet
    _context.SaveChanges();

    // Kullanıcı profil sayfasına yönlendir
    return RedirectToAction("KullaniciProfil", "Profil");
}


}
