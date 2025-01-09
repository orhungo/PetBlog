using System;
using System.ComponentModel.DataAnnotations;

namespace PetBlog.Models
{
    public class Blog
    {
        [Key]
        public int blogId { get; set; }

        [Required]
        public string baslik { get; set; } = "";

        [Required]
        public string icerik { get; set; } = "";

        public DateTime olusturmaTarihi { get; set; }
        public DateTime? guncellemeTarihi { get; set; }
        public bool aktifMi { get; set; } = true;
        public int yoneticiId { get; set; }
    }
}