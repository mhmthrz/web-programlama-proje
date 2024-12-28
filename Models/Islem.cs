using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webodev.Models
{
    public class Islem
    {
        [Key]
        public int IslemId { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        public int Ucret { get; set; }

    }
}
