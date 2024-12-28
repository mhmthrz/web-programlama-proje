using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webodev.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; } // Randevu ID
        public DateTime Tarih { get; set; } // Randevu tarihi ve saati
        [ValidateNever]
        public int? CalisanId { get; set; } // İlgili çalışan ID'si
        [ForeignKey("CalisanId")]
        [ValidateNever]
        public Calisan? Calisan { get; set; } // İlgili çalışan
        [ForeignKey("SalonId")]
        [ValidateNever]
        public int? SalonId { get; set; } // Seçilen salon ID
        public Salon? Salon { get; set; } // İlişkili salon


        [ValidateNever]
        public int? IslemId { get; set; } // İlgili işlem ID'si
        [ForeignKey("IslemId")]
        [ValidateNever]
        public Islem? Islem { get; set; } // İlgili işlem

        public string? KullaniciId  { get; set; }

        [ForeignKey("KullaniciId")]
        [ValidateNever]
        public Kullanici Kullanici { get; set; }
       
       public bool uygun{ get; set; }
    }
}
    