using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetBlog.Models
{
    [Table("Yoneticiler")]
    public class Yonetici
    {
        [Key]
        public int yoneticiId { get; set; }

        [Required]
        public string kullaniciAdi { get; set; } = "";

        [Required]
        public string sifre { get; set; } = "";

        [Required]
        public string eposta { get; set; } = "";
    }
}