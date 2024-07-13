using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetUserDto
    {
        [Display(Name = "Id")]
        public int UserId { get; set; }
        [Display(Name = "Imię i nazwisko")]
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Display(Name = "Adres mailowy")]
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Display(Name = "Rola")]
        [Required]
        public string Role { get; set; }
        [Display(Name = "Departament")]
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        [Display(Name = "Dodany przez")]
        public string CreatedByFullName { get; set; }
        [Display(Name = "Data dodania")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Zaktualizowany przez")]
        public string ModifiedByFullName { get; set; }
        [Display(Name = "Data aktualizacji")]
        public DateTime? ModifiedAt { get; set; }
    }

}
