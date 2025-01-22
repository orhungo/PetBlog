using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;


namespace PetBlog.Controllers;

public class ProfilController:Controller {

    MyAppContext _context= new MyAppContext();

    [HttpGet]
public IActionResult KullaniciProfil()
{
    int? userID = HttpContext.Session.GetInt32("userId");

    if (!userID.HasValue)
    {
        return BadRequest("Geçersiz kullanıcı ID");
    }

    // Kullanıcıyı userID ile veritabanında arama
   var kayit = _context.kayitli.FirstOrDefault(k => k.userID == userID.Value); 
   var pets = _context.petler.ToList();

 
    var petProfilModel = new ProfilPetViewModel
    {
        kullanici = kayit,
        petler = pets
    };

    return View(petProfilModel);
}


      public async Task<IActionResult> UploadPhoto(IFormFile file)
{
    if (file != null && file.Length > 0)
    {

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var photoBytes = memoryStream.ToArray();

           
            var pet = new Pet
            {
                petAd = "Kedim",
                petBilgi = "Kedimin bilgisi.",
                petFoto = photoBytes 
            };

            _context.petler.Add(pet);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Profile");
    }

    return View();
}

  public IActionResult CikisYap()
    {
    
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Kullanici");
    }
    
   [HttpGet]
    public IActionResult ProfilDuzenle(int id)
    {
        var kullanici = _context.kayitli.FirstOrDefault(k => k.userID == Convert.ToInt32(id));

        if (kullanici == null)
        {
            return NotFound(); 
        }


        return View(kullanici); 
    }

    [HttpPost]
[ValidateAntiForgeryToken]  
public IActionResult ProfilDuzenle(Kayit kayit, IFormFile userFoto)
{

    var mevcutKullanici = _context.kayitli.FirstOrDefault(k => k.userID == kayit.userID);

    if (mevcutKullanici == null)
    {
        return NotFound(); 
    }


    if (userFoto != null && userFoto.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            userFoto.CopyTo(memoryStream);
            mevcutKullanici.userFoto = memoryStream.ToArray(); 
        }
    }


    mevcutKullanici.ad = kayit.ad;
    mevcutKullanici.soyad = kayit.soyad;
    mevcutKullanici.email = kayit.email;
    mevcutKullanici.userBilgi = kayit.userBilgi;


    _context.SaveChanges();

    return RedirectToAction("KullaniciProfil", "Profil");
}
 }
  
