using System.ComponentModel.DataAnnotations;
namespace PetBlog.Models{
public class Pets{
[Key]
public int petID { get; set; }
[Required]
[StringLength(10)]
public string petAd { get; set; }="";
public string petCins { get; set; }="";
public string petTur { get; set; }="";
public int petYas {get;set;}
}
}