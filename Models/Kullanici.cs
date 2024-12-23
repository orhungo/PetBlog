using System.ComponentModel.DataAnnotations;
namespace PetBlog.Models{
public class Kullanici{
[Key]
public int userID { get; set; }
[Required]
[StringLength(10)]
public string email { get; set; }="";
public string sifre {get;set;}="";
}
}