using webodev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webodev.Models
{
    public class Calisan
    {
        [Key]
        public int Id { get; set; } // Çalışan ID

        public string Ad { get; set; }
        [ValidateNever]
        [ForeignKey("IslemId")]
        public int IslemId { get; set; } // Çalışan adı
        public Islem? Islem { get; set; } // İlgili salon
        public TimeSpan CalismaBaslangicSaati { get; set; } // Çalışma başlangıç saati
        public TimeSpan CalismaBitisSaati { get; set; } // Çalışma bitiş saati

        [ValidateNever]
        [ForeignKey("SalonId")]
        public int SalonId { get; set; } // İlgili salon ID'si
        public Salon? Salon { get; set; } // İlgili salon
            
        public ICollection<Randevu>? Randevular { get; set; } // Çalışanın randevuları

    }
}






