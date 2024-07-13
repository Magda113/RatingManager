using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetRatingDto
    {
        [Display(Name = "Id oceny")]
        public int RatingId { get; set; }
        public string Status { get; set; }
        [Display(Name = "Utworzona przez")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Zaktualizowana przez")]
        public string? ModifiedByUserName { get; set; }
        [Display(Name = "Data aktualizacji")]
        public DateTime? ModifiedAt { get; set; }
        [Display(Name = "Numer rozmowy")]
        public string CallId { get; set; }
        [Display(Name = "Imię i nazwisko użytkownika")]
        public string UserName { get; set; }
        [Display(Name = "Bezpieczeństwo")]
        public string? Safety { get; set; }
        [Display(Name = "Wiedza")]
        public string? Knowledge { get; set; }
        [Display(Name = "Komunikacja")]
        public string? Communication { get; set; }
        [Display(Name = "Kreatywność")]
        public string? Creativity { get; set; }
        [Display(Name = "Aspekty techniczne")]
        public string? TechnicalAspects { get; set; }
        [Display(Name = "Wynik")]
        public int Result { get; set; }
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
    }

}
