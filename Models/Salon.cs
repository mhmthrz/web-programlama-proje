using System.ComponentModel.DataAnnotations;

namespace webodev.Models
{
    public class Salon
    {
        [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Salon adı gereklidir.")]
    public string Ad { get; set; }

    [Required(ErrorMessage = "Telefon gereklidir.")]
    public string Telefon { get; set; }

    [Required(ErrorMessage = "Adres gereklidir.")]
    public string Adres { get; set; }

    [Required(ErrorMessage = "Açılış saati gereklidir.")]
    public TimeSpan AcilisSaati { get; set; }

    [Required(ErrorMessage = "Kapanış saati gereklidir.")]
    public TimeSpan KapanisSaati { get; set; }

    public ICollection<Calisan>? Calisanlar { get; set; } // Salon çalışanları
    public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
