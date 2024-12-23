using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using petblog.Models;
using PetBlog.Models;

namespace PetBlog.Controllers;

public class KayitController : Controller {
    
      public IActionResult Index()
    {
        return View();
    }


}