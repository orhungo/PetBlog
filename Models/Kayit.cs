using System.ComponentModel.DataAnnotations;
namespace petblog.Models{
public class Kayit{
[Key]
public int userID { get; set; }
[Required]
[StringLength(10)]
public string ad { get; set; }="";
public string soyad { get; set; }="";
public string email { get; set; }="";
public string sifre {get;set;}="";
}
}