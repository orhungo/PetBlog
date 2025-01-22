using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using System.Linq;

[Route("Anasayfa")]
public class AnasayfaController : Controller
{
    private readonly MyAppContext _context;

    
    public AnasayfaController(MyAppContext context)
    {
        _context = context;
    }

   
    [Route("Index")]
    public IActionResult Index()
    {
        // db den al
        var bloglar = _context.Bloglar
                              .OrderByDescending(b => b.olusturmaTarihi) // Tarihe göre sıralama
                              .ToList(); // Blog listesi al

        // db den view a gönder
        return View(bloglar);
    }
}
