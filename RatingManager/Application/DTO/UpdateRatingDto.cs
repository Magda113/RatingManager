using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UpdateRatingDto
    {
        [Required]
        [Display(Name = "Id")]
        public int RatingId { get; set; }

        [Required]
        [Display(Name = "Numer rozmowy")]
        public string CallId { get; set; }

        [Required]
        [Display(Name = "Imię i nazwisko użytkownika")]
        public string UserName { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Bezpieczeństwo")]
        public string? Safety { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Wiedza")]
        public string? Knowledge { get; set; }

        [Display(Name = "Komunikacja")]
        [MaxLength(1000)]
        public string? Communication { get; set; }
        [Display(Name = "Kreatywność")]
        [MaxLength(1000)]
        public string? Creativity { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Aspekty techniczne")]
        public string? TechnicalAspects { get; set; }
        [Display(Name = "Wynik")]
        [Required]
        public int Result { get; set; }
        [Required]
        [Display(Name = "Nazwa kategorii")]
        public string CategoryName { get; set; }

        [Required]
        public string Status { get; set; }
    }

}
