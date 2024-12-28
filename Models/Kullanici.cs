using Microsoft.AspNetCore.Identity;

namespace webodev.Models
{
    public class Kullanici: IdentityUser
    {
        public int Yas { get; set; } 
         public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
