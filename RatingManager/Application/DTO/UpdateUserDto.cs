using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UpdateUserDto
    {
        [Display(Name = "Id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Imię i nazwisko")]
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
    }
}
