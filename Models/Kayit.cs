using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace PetBlog.Models{
public class Kayit {
[Key]
public int userID { get; set; }
[Required]
[StringLength(10)]
public string ad { get; set; }="";
public string soyad { get; set; }="";
public string email { get; set; }="";
public string sifre {get;set;}="";
public byte[]? userFoto {get; set;}
public string userBilgi {get; set;}="";
}
}