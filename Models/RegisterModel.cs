using System.ComponentModel.DataAnnotations;

namespace webodev.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar gereklidir.")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Yaş gereklidir.")]
        [Range(1, 120, ErrorMessage = "Yaş 1 ile 120 arasında olmalıdır.")]
        public int Yas { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [RegularExpression(@"^(\d{3})\s(\d{3})\s(\d{4})$", ErrorMessage = "Geçerli bir telefon numarası giriniz (örn: 553 330 8299).")]
        public string TelefonNumarasi { get; set; }
    }
}
